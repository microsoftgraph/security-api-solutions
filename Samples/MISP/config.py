graph_auth = {
    'tenant': '<tenant id>',
    'client_id': '<client id>',
    'client_secret': '<client secret>',
}
targetProduct = "Azure Sentinel"
misp_event_filters = [
    {
        "type_attribute": ['mutex', 'filename|md5']
    }
]
action = "alert"
passiveOnly = False
days_to_expire = 5
misp_key = '<misp key>'
