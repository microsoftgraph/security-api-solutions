ATTR_MAPPING = {
    'AS': 'networkSourceAsn',
    'email-dst': 'emailRecipient',
    'email-src-display-name': 'emailSenderName',
    'email-subject': 'emailSubject',
    'email-x-mailer': 'emailXMailer',
    'filename': 'fileName',
    'malware-type': 'malwareFamilyNames',
    'mutex': 'fileMutexName',
    'port': 'networkPort',
    'published': 'isActive',
    'size-in-bytes': 'fileSize',
    'url': 'url',
    'user-agent': 'userAgent',
    'uuid': 'externalId'
}

MISP_HASH_TYPES = frozenset([
    "filename|authentihash",
    "filename|impfuzzy",
    "filename|imphash",
    "filename|md5",
    "filename|pehash",
    "filename|sha1",
    "filename|sha224",
    "filename|sha256",
    "filename|sha384",
    "filename|sha512",
    "filename|sha512/224",
    "filename|sha512/256",
    "filename|ssdeep",
    "filename|tlsh",
    "authentihash",
    "impfuzzy",
    "imphash",
    "md5",
    "pehash",
    "sha1",
    "sha224",
    "sha256",
    "sha384",
    "sha512",
    "sha512/224",
    "sha512/256",
    "ssdeep",
    "tlsh",
])

MISP_SPECIAL_CASE_TYPES = frozenset([
    *MISP_HASH_TYPES,
    'ip-dst',
    'ip-src',
    'domain|ip',
    'email-src',
    'ip-dst|port',
    'ip-src|port'
])

MISP_ACTIONABLE_TYPES = frozenset([
    *ATTR_MAPPING.keys(),
    *MISP_SPECIAL_CASE_TYPES
])

CLIENT_ID = 'client_id'
CLIENT_SECRET = 'client_secret'
TENANT = 'tenant'
ACCESS_TOKEN = 'access_token'
GRAPH_TI_INDICATORS_URL = 'https://graph.microsoft.com/beta/security/tiindicators'
GRAPH_BULK_POST_URL = f'{GRAPH_TI_INDICATORS_URL}/submitTiIndicators'
GRAPH_BULK_DEL_URL = f'{GRAPH_TI_INDICATORS_URL}/deleteTiIndicators'
LOG_DIRECTORY_NAME = 'logs'
EXISTING_INDICATORS_HASH_FILE_NAME = 'existing_indicators_hash.json'
EXPIRATION_DATE_TIME = 'expirationDateTime'
EXPIRATION_DATE_FILE_NAME = 'expiration_date.txt'
INDICATOR_REQUEST_HASH = 'indicatorRequestHash'
TARGET_PRODUCT_BULK_SUPPORT = ['Azure Sentinel']
TARGET_PRODUCT_NON_BULK_SUPPORT = ['Microsoft Defender ATP']

EVENT_MAPPING = {
    'date': 'firstReportedDateTime',
    'timestamp': 'lastReportedDateTime',
    'info': 'description',
    'uuid': 'externalId'
}

REQUIRED_GRAPH_METADATA = frozenset([
    "threatType",
    "tlpLevel",
    "description",
    "expirationDateTime",
    "targetProduct",
])

OPTIONAL_GRAPH_METADATA = frozenset([
    "activityGroupNames",
    "additionalInformation",
    "confidence",
    "diamondModel",
    "externalId",
    "isActive",
    "killChain",
    "knownFalsePositives",
    "lastReportedDateTime",
    "malwareFamilyNames",
    "passiveOnly",
    "severity",
    "tags",
])

GRAPH_OBSERVABLES = frozenset([
    "emailEncoding",
    "emailLanguage",
    "emailRecipient",
    "emailSenderAddress",
    "emailSenderName",
    "emailSourceDomain",
    "emailSourceIPAddress",
    "emailSubject",
    "emailXMailer",
    "fileCompileDateTime",
    "fileCreationDateTime",
    "fileHashType",
    "fileHashValue",
    "fileMutexName",
    "fileName",
    "filePacker",
    "filePath",
    "fileSize",
    "fileType",
    "domainName",
    "networkIPv4",
    "networkIPv6",
    "networkPort",
    "networkDestinationAsn",
    "networkDestinationCidrBlock",
    "networkDestinationIPv4",
    "networkDestinationIPv6",
    "networkDestinationPort",
    "networkProtocol",
    "networkSourceAsn",
    "networkSourceCidrBlock",
    "networkSourceIPv4",
    "networkSourceIPv6",
    "networkSourcePort",
    "url",
    "userAgent",
])