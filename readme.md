<h1 align="left"> Solution </h1>

# Description

This backend application contains 
- Shipping API for client side of the application
- Shipping Domain for Domain Logic
- Shipping Infrastructure for infrastructure of the application

### Shipping API
- It has only one controller which is VehiclesController and handles the post method of the endpoint v1/vehicles/{vehiclePlate}/distribution-request 
and send DistributeCommand to mediatr to distribute shipments
    Returns 200 Ok if everything Ok in the Server.
    Returns 400 Bad Request if request cannot be handled for the api, invalid request or any thrown Shipping domain exception logic.
    Returns 500 Internal Server Error if something is wrong in the server.

- It has application logic, which has command and event handlers which handles commands that are sent for this logic mediator pattern and mediatR framework are used
.And application layer has models which are used by accepting request and sending responses.

- It has infrastructure side of the API, so it means middlewares and modules which are needed to be registered as Services

### Shipping Domain
- It has the Aggregate Model, Value Objects, Enums, Bounded Context logic and Policies for delivery rules, 
and Interfaces for the contracts which are used with other modules in common.
- For Delivery Point Rules, strategy design pattern is used
- It has exceptions folder which contains domain exception which is currently ShippingDomainException
- It has ShipmentUnloadFailedDomainEvent which is sent when a package or sack is failed to unload to desired Delivery Point.

### Shipping Infrastructure
- It has Infrastructure Logic of the application, for example Database Logic and how data will be stored and Log Services.
- Repositories for getting data from database which is implements IRepository contract which is created in Domain Layer.
- ShipmentDbContext has the entities which are stored in database and their logic.
- This application use InMemoryDatabase which is provided by EntityFrameworkCore.

### Shipping Unit Tests
- Application has unit tests and its coverage is 82.6%(668 of 808)
- XUnit Framework is used for creating unit tests
- Builder pattern is used for constructing new Package and Sack

## Used Technologies and Architectures
- .Net 7
- Domain Driven Design
- CQRS (MediatR)
- EF Core as a ORM and InMemoryDatabase
- Docker for containerization
- UnitOfWork and Repository patterns...

## Build and Testing
- git clone https://github.com/DevelopmentHiring/BerkAlgul.git
- docker-compose up -d
- curl -L -X GET 'http://localhost:8080/health'
- curl -L -X POST 'http://localhost:8080/v1/vehicles/34TL34/distribution-request ' -H 'Content-Type: application/json' --data-raw '{"route":[{"deliveryPoint":1,"deliveries":[{"barcode":"P7988000121"},{"barcode":"P7988000122"},{"barcode":"P7988000123"},{"barcode":"P8988000121"},{"barcode":"C725799"}]},{"deliveryPoint":2,"deliveries":[{"barcode":"P8988000123"},{"barcode":"P8988000124"},{"barcode":"P8988000125"},{"barcode":"C725799"}]},{"deliveryPoint":3,"deliveries":[{"barcode":"P9988000126"},{"barcode":"P9988000127"},{"barcode":"P9988000128"},{"barcode":"P9988000129"},{"barcode":"P9988000130"}]}]}'

- You can use swagger for testing as well at the endpoint 'http://localhost:8080/swagger/index.html', it is nice for api testing.
- All the data mentioned above is seeded to in memory database when deploying the application at the first time.

### Some improvenet needed to be done
- Endpoints should not have verbs, so distribute endpoint is needed to be renamed as distributions