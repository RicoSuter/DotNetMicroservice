# DotNetMicroservice

**Preview**

This repository contains a sample and blueprint logical microservice implementation written in .NET Core.

The microservice is composed of three individual parts which can be run individually as separate processes or in a single process (scaled-down version):

- Web API (http endpoint)
- Create order processor (queue listener)
- Complete order processor (queue listener)

Run all parts in a single process (local development, development environment)

```
dotnet run
```

Run as multiple processes (production environment):

```
dotnet run --mode webapi
dotnet run --mode createorders
dotnet run --mode completeorders
```

If no configuration is provided, then all repositories are using an in-memory storage for local testing purposes.
The in-memory configuration only works when running in a single process because messages are passed in-process. 

If you don't like having multiple processors in a single project, you can also create individual app projects per processor.

## Projects

- DotNetMicroservice
	- The console application hosting one or all parts
	- This project also contains the web api controllers and the public DTO classes
- DotNetMicroservice.Processes
	- The background processes, in this sample the message queue listeners
	- The background processing classes are hosted in the DotNetMicroservice project
	- We use message queue listener interfaces so that we can also use in-memory queues for integration testing, see [Namotion.Messaging](https://github.com/RicoSuter/Namotion.Messaging)
- DotNetMicroservice.Services
    - Contains the business service classes which implement the actual business requirements
	- Usually these service classes use other service classes and repository classes via dependency injection
- DotNetMicroservice.Repositories
    - Contains the repositories which handle data storage
	- The repositories and services are split into two projects so that repositories do not have access to the service interfaces, i.e. only services are allowed to use repositories.
	- A repository is an abstraction of the storage and required to be able to switch between in-memory implementations and actual implementations (e.g. MongoDB)
	- To avoid multiple repository implementations, you could use a storage abstractions which already provides this functionality, e.g. [Namotion.Storage](https://github.com/RicoSuter/Namotion.Storage)
- DotNetMicroservice.Domain
    - The domain classes which should be storage and technology independent (POCOs)
- DotNetMicroservice.Client
    - Contains generated clients to access the public web api ([NSwag](https://github.com/RicoSuter/NSwag))
	- The clients are used in the DotNetMicroservice.Tests project to test scenarios and can be published as a public SDK
- DotNetMicroservice.Tests
    - Implementation of unit and integration tests

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
With the environment variable "apiEndpoint" you can run the same integration tests against a deployed version (e.g. in a CI/CD scenario).
