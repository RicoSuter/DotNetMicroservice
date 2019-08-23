# DotNetMicroservice

**Preview**

A sample (logical) microservice implementation in .NET

The microservice is composed of three individual parts: 

- Web API (http endpoint)
- Create order processor (queue listener)
- Complete order processor (queue listener)

These parts can be run individually as separate processes or in a single process (scaled-down version): 

```
dotnet run
```

Run as multiple processes:

```
dotnet run --mode webapi
dotnet run --mode createorders
dotnet run --mode completeorders
```

The in-memory configuration only works when running in a single process (messages are passed in-process).

## The sample order process

1. Client wants to order something: 
    - Call web api to create a new order => enqueue create order message
    - CreateOrderMessageProcessor (async)
	    - Process create order message => remove quantity from availabe stock and store order
2. Employee shipped the items and got a parcel number from the postal service: 
    - Call web api to complete pending order => enqueue complete order message
	- CompleteOrderMessageProcessor (async)
	    - Process complete order message => change order state to completed

This whole scenario is tested in `WhenOrderIsCreatedAndCompleted_ThenParcelNumberIsAvailable`.

## Integration tests

By default the integration tests are run against an in-memory test server with all parts. 
With the environment variable "apiEndpoint" you can run the integration tests against a deployed version (e.g. in a CI/CD scenario).
