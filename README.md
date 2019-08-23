# DotNetMicroservice

A sample (logical) microservice implementation in .NET

The microservice is composed of three individual parts: 

- Web API (http endpoint)
	- `DotNetMicroservice.Startup`
	- `DotNetMicroservice.Controllers`
- Create order processor (queue listener)
    - `DotNetMicroservice.Processes.CreateOrderMessageProcessorService`
- Complete order processor (queue listener)
    - `DotNetMicroservice.Processes.CompleteOrderMessageProcessorService`

These parts can be run individually as separate processes or in a single process (scaled-down version).

## The sample order process

1. Client wants to order something: 
    - Call web api to create a new order => enqueue create order message
    - CreateOrderMessageProcessorService (async)
	    - Process create order message => remove quantity from availabe stock and store order
2. Employee shipped the items and got a parcel number from the postal service: 
    - Call web api to complete pending order => enqueue complete order message
	- CompleteOrderMessageProcessorService (async)
	    - Process complete order message => change order state to completed

This whole scenario is tested in `WhenOrderIsCreatedAndCompleted_ThenParcelNumberIsAvailable`.