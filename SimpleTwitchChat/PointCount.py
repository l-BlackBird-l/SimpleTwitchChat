from exceptions import StreamerDoesNotExistException
from twitch_data import get_channel_id, post_gql_request
import sys

# For documentation on Twitch GraphQL API see:
# https://www.apollographql.com/docs/
# https://github.com/mauricew/twitch-graphql-api
# Full list of available methods: https://azr.ivr.fi/schema/query.doc.html (a bit outdated)


# Load the amount of current points for a channel, check if a bonus is available
def load_channel_points_context(streamer_login):
    json_data = {"operationName": "ChannelPointsContext",
                 "variables": {"channelLogin": streamer_login},
                 "extensions": {"persistedQuery": {"version": 1, "sha256Hash": "9988086babc615a918a1e9a722ff41d98847acac822645209ac7379eecb27152"}}}
    response = post_gql_request(json_data)
    if response["data"]["community"] is None:
        raise StreamerDoesNotExistException
    community_points = response["data"]["community"]["channel"]["self"]["communityPoints"]
    initial_balance = community_points["balance"]
    print(f"{initial_balance}")
    available_claim = community_points["availableClaim"]


def main():
	load_channel_points_context(sys.argv[1])

if __name__ == "__main__":
    main()


