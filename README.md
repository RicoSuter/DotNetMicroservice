# DotNetMicroservice

A sample (logical) microservice implementation in .NET

The microservice is composed of three individual parts: 

- Web API (http endpoint)
- Create order processor (queue listener)
- Complete order processor (queue listener)

These parts can be run individually as separate processes or in a single process (scaled-down version).
