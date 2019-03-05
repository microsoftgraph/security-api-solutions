import requests
import config
import datetime
import os
import json
import copy
from constants import *


class RequestManager:
    def __init__(self):
        self.del_count = 0
        try:
            self.existing_indicators_hash_fd = open(EXISTING_INDICATORS_HASH_FILE_NAME, 'r+')
            self.existing_indicators_hash = json.load(self.existing_indicators_hash_fd)
        except FileNotFoundError:
            self.existing_indicators_hash_fd = open(EXISTING_INDICATORS_HASH_FILE_NAME, 'w')
            self.existing_indicators_hash = {}
        try:
            self.expiration_date_fd = open(EXPIRATION_DATE_FILE_NAME, 'r+')
            self.expiration_date = self.expiration_date_fd.read()
        except FileNotFoundError:
            self.expiration_date_fd = open(EXPIRATION_DATE_FILE_NAME, 'w')
            self.expiration_date = self._get_expiration_date_from_config()
        if self.expiration_date <= datetime.datetime.utcnow().strftime('%Y-%m-%d'):
            self.existing_indicators_hash = {}
            self.expiration_date = self._get_expiration_date_from_config()
        self.hash_of_indicators_to_delete = copy.deepcopy(self.existing_indicators_hash)
        access_token = self._get_access_token(
            config.graph_auth[TENANT],
            config.graph_auth[CLIENT_ID],
            config.graph_auth[CLIENT_SECRET])
        self.headers = {"Authorization": f"Bearer {access_token}"}
        self.headers_expiration_time = int(datetime.datetime.now().timestamp()) + 3500
        self.success_count = 0
        self.error_count = 0
        self.indicators_to_be_sent = []
        self.indicators_to_be_sent_size = 0
        if not os.path.exists(LOG_DIRECTORY_NAME):
            os.makedirs(LOG_DIRECTORY_NAME)

    @staticmethod
    def _get_expiration_date_from_config():
        return (datetime.datetime.utcnow() + datetime.timedelta(config.days_to_expire)).strftime('%Y-%m-%d')

    @staticmethod
    def _get_access_token(tenant, client_id, client_secret):
        data = {
            CLIENT_ID: client_id,
            'scope': 'https://graph.microsoft.com/.default',
            CLIENT_SECRET: client_secret,
            'grant_type': 'client_credentials'
        }
        access_token = requests.post(
            f'https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token',
            data=data
        ).json()[ACCESS_TOKEN]
        return access_token

    @staticmethod
    def read_tiindicators():
        access_token = RequestManager._get_access_token(
            config.graph_auth[TENANT],
            config.graph_auth[CLIENT_ID],
            config.graph_auth[CLIENT_SECRET])
        print(json.dumps(requests.get(
            GRAPH_TI_INDICATORS_URL,
            headers={"Authorization": f"Bearer {access_token}"}
            ).json(), indent=2))

    @staticmethod
    def _get_request_hash(request):
        return str(hash(frozenset({
            k: str(v) for k, v in request.items()
            if k != 'expirationDateTime' and k != 'lastReportedDateTime'
        }.items())))

    def _log_post(self, response):
        for value in response['value']:
            if "Error" in value:
                self.error_count += 1
                log_file_name = f"{self._get_datetime_now()}_error_{value[INDICATOR_REQUEST_HASH]}.json"
            else:
                self.success_count += 1
                self.existing_indicators_hash[value[INDICATOR_REQUEST_HASH]] = value['id']
                if not config.verbose_log:
                    continue
                log_file_name = f"{self._get_datetime_now()}{value[INDICATOR_REQUEST_HASH]}.json"
            print(log_file_name)
            print(json.dumps(value, indent=2))
            print()
            json.dump(value, open(f'{LOG_DIRECTORY_NAME}/{log_file_name}', 'w'), indent=2)

    @staticmethod
    def _get_datetime_now():
        return str(datetime.datetime.now()).replace(' ', '_')

    def conclude(self):
        self._post_to_graph()

        self._del_indicators_no_longer_exist()

        self.expiration_date_fd.seek(0)
        self.expiration_date_fd.write(self.expiration_date)
        self.expiration_date_fd.truncate()

        self.existing_indicators_hash_fd.seek(0)
        json.dump(self.existing_indicators_hash, self.existing_indicators_hash_fd, indent=2)
        self.existing_indicators_hash_fd.truncate()

        self._print_summary()

    def _del_indicators_no_longer_exist(self):
        for hash_of_indicator_to_delete, tiindicator_id in self.hash_of_indicators_to_delete.items():
            self.existing_indicators_hash.pop(hash_of_indicator_to_delete, None)
            self._del_from_graph(tiindicator_id)

    def _print_summary(self):
        print()
        print(f"requests sent:      {str(self.success_count + self.error_count).rjust(5)}")
        print(f"response success:   {str(self.success_count).rjust(5)}")
        print(f"response error:     {str(self.error_count).rjust(5)}")
        print(f"indicators deleted: {str(self.del_count).rjust(5)}")

    def _del_from_graph(self, tiindicator_id):
        response_content = str(requests.delete(f"{GRAPH_TI_INDICATORS_URL}/{tiindicator_id}", headers=self.headers).content)
        self._log_del(tiindicator_id, response_content)

    def _post_to_graph(self):
        request_body = {'value': self.indicators_to_be_sent}
        response = requests.post(GRAPH_BULK_POST_URL, headers=self.headers, json=request_body).json()
        self.indicators_to_be_sent = []
        self._log_post(response)

    def handle_indicator(self, indicator):
        self.update_headers_if_expired()
        indicator[EXPIRATION_DATE_TIME] = self.expiration_date
        indicator_hash = self._get_request_hash(indicator)
        indicator[INDICATOR_REQUEST_HASH] = indicator_hash
        self.hash_of_indicators_to_delete.pop(indicator_hash, None)
        if indicator_hash not in self.existing_indicators_hash:
            self.indicators_to_be_sent.append(indicator)
        if len(self.indicators_to_be_sent) >= 100:
            self._post_to_graph()

    def _log_del(self, tiindicator_id, response_content):
        log_json_body = {
            'tiindicator_id': tiindicator_id,
            'response_content': response_content
        }
        self.del_count += 1
        log_file_name = f"{self._get_datetime_now()}.json"
        print(log_file_name)
        print(json.dumps(log_json_body, indent=2))
        print()
        json.dump(log_json_body, open(f'{LOG_DIRECTORY_NAME}/{log_file_name}', 'w'), indent=2)

    def update_headers_if_expired(self):
        if int(datetime.datetime.now().timestamp()) > self.headers_expiration_time:
            access_token = self._get_access_token(
                config.graph_auth[TENANT],
                config.graph_auth[CLIENT_ID],
                config.graph_auth[CLIENT_SECRET])
            self.headers = {"Authorization": f"Bearer {access_token}"}
