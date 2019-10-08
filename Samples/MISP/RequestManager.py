import requests
import config
import datetime
import os
import json
import copy
from constants import *


class RequestManager:
    """A class that handles submitting TiIndicators to MS Graph API

    to use the class:
        with RequestManager() as request_manager:
            request_manager.handle_indicator(tiindicator)

    """

    RJUST = 5

    def __init__(self, total_indicators):
        self.total_indicators = total_indicators

    def __enter__(self):
        try:
            self.existing_indicators_hash_fd = open(EXISTING_INDICATORS_HASH_FILE_NAME, 'r+')
            self.existing_indicators_hash = json.load(self.existing_indicators_hash_fd)
        except (FileNotFoundError, json.decoder.JSONDecodeError):
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
        self.headers = {"Authorization": f"Bearer {access_token}", 'user-agent': 'MISP/1.0'}
        self.headers_expiration_time = self._get_timestamp() + 3500
        self.success_count = 0
        self.error_count = 0
        self.del_count = 0
        self.indicators_to_be_sent = []
        self.indicators_to_be_sent_size = 0
        self.start_time = self.last_batch_done_timestamp = self._get_timestamp()
        if not os.path.exists(LOG_DIRECTORY_NAME):
            os.makedirs(LOG_DIRECTORY_NAME)
        return self

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
        self._clear_screen()
        cur_batch_success_count = cur_batch_error_count = 0
        for value in response['value']:
            if "Error" in value:
                self.error_count += 1
                cur_batch_error_count += 1
                log_file_name = f"{self._get_datetime_now()}_error_{value[INDICATOR_REQUEST_HASH]}.json"
            else:
                self.success_count += 1
                cur_batch_success_count += 1
                self.existing_indicators_hash[value[INDICATOR_REQUEST_HASH]] = value['id']
                if not config.verbose_log:
                    continue
                log_file_name = f"{self._get_datetime_now()}_{value[INDICATOR_REQUEST_HASH]}.json"
            json.dump(value, open(f'{LOG_DIRECTORY_NAME}/{log_file_name}', 'w'), indent=2)

        print('sending security indicators to Microsoft Graph Security\n')
        print(f'{self.total_indicators} indicators are parsed from misp events. Only those that do not exist in Microsoft Graph Security will be sent.\n')
        print(f"current batch indicators sent:  {str(cur_batch_success_count + cur_batch_error_count).rjust(self.RJUST)}")
        print(f"current batch response success: {str(cur_batch_success_count).rjust(self.RJUST)}")
        print(f"current batch response error:   {str(cur_batch_error_count).rjust(self.RJUST)}\n")
        print(f"total indicators sent:          {str(self._get_total_indicators_sent()).rjust(self.RJUST)}")
        print(f"total response success:         {str(self.success_count).rjust(self.RJUST)}")
        print(f"total response error:           {str(self.error_count).rjust(self.RJUST)}\n")
        cur_batch_took = self._get_timestamp() - self.last_batch_done_timestamp
        self.last_batch_done_timestamp = self._get_timestamp()
        print(f'current batch took:   {round(cur_batch_took, 2):{6}} seconds')
        avg_speed = self._get_total_indicators_sent() / (self.last_batch_done_timestamp - self.start_time)
        print(f'average speed so far: {round(avg_speed, 2):{6}} indicators per second')
        time_left = (self.total_indicators - self._get_total_indicators_sent()) / avg_speed
        print(f'estimated time left:  {round(time_left, 2):{6}} seconds')

    @staticmethod
    def _get_datetime_now():
        return str(datetime.datetime.now()).replace(' ', '_')

    def __exit__(self, exc_type, exc_val, exc_tb):
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
        indicators = list(self.hash_of_indicators_to_delete.values())
        self.del_count = len(indicators)
        for i in range(0, len(indicators), 100):
            request_body = {'value': indicators[i: i+100]}
            response = requests.post(GRAPH_BULK_DEL_URL, headers=self.headers, json=request_body).json()
            log_file_name = f"del_{self._get_datetime_now()}.json"
            print(log_file_name)
            print(json.dumps(response, indent=2))
            print()
            json.dump(response, open(f'{LOG_DIRECTORY_NAME}/{log_file_name}', 'w'), indent=2)
        for hash_of_indicator_to_delete in self.hash_of_indicators_to_delete.keys():
            self.existing_indicators_hash.pop(hash_of_indicator_to_delete, None)

    def _print_summary(self):
        self._clear_screen()
        print('script finished running\n')
        print(f"total indicators sent:    {str(self._get_total_indicators_sent()).rjust(self.RJUST)}")
        print(f"total response success:   {str(self.success_count).rjust(self.RJUST)}")
        print(f"total response error:     {str(self.error_count).rjust(self.RJUST)}")
        print(f"total indicators deleted: {str(self.del_count).rjust(self.RJUST)}")

    def _post_to_graph(self):
        request_body = {'value': self.indicators_to_be_sent}
        response = requests.post(GRAPH_BULK_POST_URL, headers=self.headers, json=request_body).json()
        self.indicators_to_be_sent = []
        self._log_post(response)

    def handle_indicator(self, indicator):
        self._update_headers_if_expired()
        indicator[EXPIRATION_DATE_TIME] = self.expiration_date
        indicator_hash = self._get_request_hash(indicator)
        indicator[INDICATOR_REQUEST_HASH] = indicator_hash
        self.hash_of_indicators_to_delete.pop(indicator_hash, None)
        if indicator_hash not in self.existing_indicators_hash:
            self.indicators_to_be_sent.append(indicator)
        if len(self.indicators_to_be_sent) >= 100:
            self._post_to_graph()

    def _update_headers_if_expired(self):
        if self._get_timestamp() > self.headers_expiration_time:
            access_token = self._get_access_token(
                config.graph_auth[TENANT],
                config.graph_auth[CLIENT_ID],
                config.graph_auth[CLIENT_SECRET])
            self.headers = {"Authorization": f"Bearer {access_token}"}

    @staticmethod
    def _clear_screen():
        if os.name == 'posix':
            os.system('clear')
        else:
            os.system('cls')

    @staticmethod
    def _get_timestamp():
        return datetime.datetime.now().timestamp()

    def _get_total_indicators_sent(self):
        return self.error_count + self.success_count
