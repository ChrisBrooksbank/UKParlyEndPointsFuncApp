# UKParlyEndPointsFuncApp

Azure Functions that support the [UKParliamentEndpoints](https://github.com/ChrisBrooksbank/UKParliamentEndpoints) API.

## Overview

This project contains .NET 8 Azure Functions that call the UK Parliament Endpoints API to check whether stored endpoint URLs are still responding.

## Functions

### FunctionCheck

Simple HTTP-triggered function that returns a check message.

### PingNewOrFailed

Pings Parliament endpoints that have never been checked or whose last ping was not successful, up to a maximum of 500 endpoints.

Triggered on a timer twice a day, at 10 AM and 4 PM.

### PingAll

Pings all Parliament endpoints, up to a maximum of 500 endpoints.

Triggered on a timer every weekday morning at 5 AM.

## Run locally

Prerequisites:

- .NET 8 SDK
- Azure Functions Core Tools

```bash
dotnet restore
func start
```

## Build

```bash
dotnet build UkParlyEndPointsFuncApp.sln
```

## Configuration

Function app settings should provide the API base URL and any credentials required by the endpoint-checking services. Keep production values in Azure configuration or local development secrets rather than committing them to the repository.
