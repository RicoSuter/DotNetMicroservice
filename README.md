# DotNetMicroservice

A sample (logical) microservice implementation in .NET

The microservice is composed of three individual parts: 

- webapi (http endpoint)
- create order processor (queue listener)
- complete order processor (queue listener)

These parts can be run individually as separate processors or in a single process (scaled-down version).
