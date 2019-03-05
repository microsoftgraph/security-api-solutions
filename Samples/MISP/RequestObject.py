from constants import *


class RequestObject:
    """A class that parses attribute from misp to the format consumable by MS Graph API

    to use the class:
        request_object = RequestObject(attr) # this reads in the attr and parses it
        # then use request.__dict__ to get the parsed dict

    """
    def __init__(self, attr):
        mapping = ATTR_MAPPING.get(attr['type'])
        if mapping is not None:
            setattr(self, mapping, attr['value'])
        if attr['type'] in MISP_SPECIAL_CASE_TYPES:
            self._handle_special_cases(attr)
        self.tags = [tag['name'].strip() for tag in attr.get("Tag", [])]
        for tag in self.tags:
            if 'diamond-model:' in tag:
                self.diamondModel = tag.split(':')[1]

    def _handle_ip(self, attr, attr_type, graph_v4_name, graph_v6_name):
        if attr['type'] == attr_type:
            if '.' in attr['value']:
                setattr(self, graph_v4_name, attr['value'])
            else:
                setattr(self, graph_v6_name, attr['value'])

    def _aggregated_handle_ip(self, attr):
        self._handle_ip(attr, 'ip-dst', 'networkDestinationIPv4', 'networkDestinationIPv6')
        self._handle_ip(attr, 'ip-src', 'networkSourceIPv4', 'networkSourceIPv6')

    def _handle_file_hash(self, attr):
        if attr['type'] in MISP_HASH_TYPES:
            if 'filename|' in attr['type']:
                self.fileHashType = attr['type'].split('|')[1]
                self.fileName, self.fileHashValue = attr['value'].split('|')
            else:
                self.fileHashType = attr['type']
                self.fileHashValue = attr['value']
            if self.fileHashType not in ['sha1', 'sha256', 'md5', 'authenticodeHash256', 'lsHash', 'ctph']:
                self.fileHashType = "unknown"

    def _handle_email_src(self, attr):
        if attr['type'] == 'email-src':
            self.emailSenderAddress = attr['value']
            self.emailSourceDomain = attr['value'].split('@')[1]

    def _handle_ip_port(self, attr):
        if attr['type'] == 'ip-dst|port' or attr['type'] == 'ip-src|port':
            ip = attr['value'].split('|')[0]
            port = attr['value'].split('|')[1]
            if attr['type'] == 'ip-dst|port':
                self.networkDestinationPort = port
                if '.' in attr['value']:
                    self.networkDestinationIPv4 = ip
                else:
                    self.networkDestinationIPv6 = ip
            elif attr['type'] == 'ip-src|port':
                self.networkSourcePort = port
                if '.' in attr['value']:
                    self.networkSourceIPv4 = ip
                else:
                    self.networkSourceIPv6 = ip

    def _handle_special_cases(self, attr):
        self._aggregated_handle_ip(attr)
        self._handle_domain_ip(attr)
        self._handle_email_src(attr)
        self._handle_ip_port(attr)
        self._handle_file_hash(attr)

    def _handle_domain_ip(self, attr):
        if attr['type'] == 'domain|ip':
            self.domainName, ip = attr['value'].split('|')
            if '.' in ip:
                self.networkIPv4 = ip
            else:
                self.networkIPv6 = ip
