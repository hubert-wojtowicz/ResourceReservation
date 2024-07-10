# ResourceReservation
Microservice for Resource exclusive reservation.

## Definitons
`Party` - any identity.
`Resource` - it can be any item that may be assigned to one and only one Party at the same time.
`Reservation` - it does define bond between Party and Resource.

## Main bussines question this api answers
1. Is `Resource` availiable for `Party` exclusive reservation?
2. What reservation does `Party` posses?

## What is beyound the scope of this api
1. Any question in relation to the time of reservation are not in responsibility of this api. This should be delegated to seareate service.
2. Any questions about reservation history are beyond scope of this application.

## Requirements
1. Can not delete Resource that is under reservation.
2. Can not reserve the samme Resource twice.
3. Any Get operation returns paginated result.
4. `Resource` Tag consist of 2 values delimited by `=` sign.


# App startup 

Please run migrations (EF Code First) against Sql Server Express database. Connection string is defined in `appsettings.json`.
$ cd src\ReservationApi.Infrastructure
$ dotnet ef --startup-project ..\ReservationApi\ReservationApi.csproj database update --project .\ReservationApi.Infrastructure.csproj

# Task

Create an application (in .NET 6) with a REST/json API that retrieves data from at least 2 sources (repositories), based on the data provided in the request, e.g. by using query parameters like ?source=A,B,D or based on the body of the request.

While implementing, think about

* SOLID principles
* REST API design
* Swagger documentation
* Validation
* Repository pattern
* Resilience
* Caching
* Monitoring
* Logging
* Unit tests (not necessary to achieve 100% code coverage, but we just want to see how you approach it)

The data for the repositories can come from a static file, memory cache, or whatever suits you best. The data could be Movies, e.g. because that is a familiar domain and data is available in files on the Internet.

Add comments to the code to explain decisions, constructs, etc. wherever you think it is necessary.

# Used CLI commands

- `dotnet new webapi --name ReservationApi --framework net6.0 --auth none -minimal`

- `dotnet ef --startup-project ..\ReservationApi\ReservationApi.csproj migrations add Init`

- `dotnet ef --startup-project ..\ReservationApi\ReservationApi.csproj database update --project .\ReservationApi.Infrastructure.csproj`