# UKParliamentEndPointsFuncs

Supporting Azure functions for the [UKParliamentEndPoints API](https://ukparliamentendpoints-services.azurewebsites.net/swagger/index.html).

## Overview

This project contains Azure Functions designed to interact with the UKParliamentEndPoints API, 
facilitating automated tasks such as pinging endpoints to ensure their availability.

## Functions

### FunctionCheck
[FunctionCheck](https://ukparlyendpointsfuncapp.azurewebsites.net/api/Check?name=ChrisB)

Simple function that returns a check message.
Triggers with a GET http request.

### PingNew

Pings all unpinged parliament endpoints (max of 100)
Triggers on a timer which fires every 20 minutes.

### PingAll

Pings all parliament endpoints (max of 100)
Triggers on a timer which fires every hour, on the hour, from 6 AM to 10 PM , Monday to Saturday.