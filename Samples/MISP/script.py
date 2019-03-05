from pymisp import PyMISP
import config
from collections import defaultdict
import datetime
from RequestManager import RequestManager
from RequestObject import RequestObject
from constants import *
import sys
from functools import reduce


def _get_events():
    misp = PyMISP(MISP_DOMAIN, config.misp_key)
    if len(config.misp_event_filters) == 0:
        return [event['Event'] for event in misp.search(values="")['response']]
    events_for_each_filter = [
        [event['Event'] for event in misp.search(**event_filter)['response']]
        for event_filter in config.misp_event_filters
    ]
    event_ids_for_each_filter = [set(event['id'] for event in events) for events in events_for_each_filter]
    event_ids_intersection = reduce((lambda x, y: x & y), event_ids_for_each_filter)
    return [event for event in events_for_each_filter[0] if event['id'] in event_ids_intersection]


def _graph_post_request_body_generator(parsed_events):
    for event in parsed_events:
        request_body_metadata = {
            **{field: event[field] for field in REQUIRED_GRAPH_METADATA},
            **{field: event[field] for field in OPTIONAL_GRAPH_METADATA if field in event},
            'action': config.action,
            'passiveOnly': config.passiveOnly,
            'threatType': 'watchlist',
            'targetProduct': config.targetProduct,
        }
        for request_object in event['request_objects']:
            request_body = {
                **request_body_metadata.copy(),
                **request_object.__dict__,
                'tags': request_body_metadata.copy()['tags'] + request_object.__dict__['tags']
            }
            yield request_body


def _handle_timestamp(parsed_event):
    parsed_event['lastReportedDateTime'] = str(
        datetime.datetime.fromtimestamp(int(parsed_event['lastReportedDateTime'])))


def _handle_diamond_model(parsed_event):
    for tag in parsed_event['tags']:
        if 'diamond-model:' in tag:
            parsed_event['diamondModel'] = tag.split(':')[1]


def _handle_tlp_level(parsed_event):
    for tag in parsed_event['tags']:
        if 'tlp:' in tag:
            parsed_event['tlpLevel'] = tag.split(':')[1]
    if 'tlpLevel' not in parsed_event:
        parsed_event['tlpLevel'] = 'red'


def main():
    if '-r' in sys.argv:
        RequestManager.read_tiindicators()
        sys.exit()
    config.verbose_log = ('-v' in sys.argv)
    events = _get_events()
    parsed_events = list()
    for event in events:
        parsed_event = defaultdict(list)

        for key, mapping in EVENT_MAPPING.items():
            parsed_event[mapping] = event.get(key, "")
        parsed_event['tags'] = [tag['name'].strip() for tag in event.get("Tag", [])]
        _handle_diamond_model(parsed_event)
        _handle_tlp_level(parsed_event)
        _handle_timestamp(parsed_event)

        for attr in event['Attribute']:
            if attr['type'] == 'threat-actor':
                parsed_event['activityGroupNames'].append(attr['value'])
            if attr['type'] == 'comment':
                parsed_event['description'] += attr['value']
            if attr['type'] in MISP_ACTIONABLE_TYPES:
                parsed_event['request_objects'].append(RequestObject(attr))

        parsed_events.append(parsed_event)
    del events

    request_manager = RequestManager()
    for request_body in _graph_post_request_body_generator(parsed_events):
        request_manager.handle_indicator(request_body)
    request_manager.conclude()


if __name__ == '__main__':
    main()
