# UKParliamentEndPointsFuncs
Supporting Azure functions for the [UKParliamentEndPoints API](https://ukparliamentendpoints-services.azurewebsites.net/swagger/index.html).

## Overview
This project contains Azure Functions designed to interact with the UKParliamentEndPoints API, 
facilitating automated tasks such as pinging endpoints to ensure they are working.

## Functions

### FunctionCheck
[FunctionCheck](https://ukparlyendpointsfuncapp.azurewebsites.net/api/Check?name=ChrisB)

Simple function that returns a check message.
Triggers with a GET http request.

### PingNewOrFailed
Pings parliament endpoints which have never been pinged, or whose last ping was not successful (max of 500)
Triggers on a timer which fires twice a day, at 10 AM and 4 PM.

### PingAll
Pings all parliament endpoints (max of 500)
Triggers on a timer which fires every every week day morning at 5 AM.