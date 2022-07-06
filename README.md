# TP. Net - Shopping List - Final Project

This project is a Final Project of Teleperformance Net Bootcamp which includes Asp.Net framework with Restful APIs, Database Designs and neccessary Services.
To be able to make the project Clean Architecture Design has been used.

## Requirements
- Creating a Shopping List which is able to control by users.
- Users can be able signup and login. Then, they can be able to add, update, delete or search the lists and list items.
- Lists must have categories and items inside of it. Users must have authentication to use the app.
- Admin user can able to see all the lists and completed lists.
- PostgreSQL  must be used for normal actions but MongoDB for archiving the lists which sent by the Rabbitmq message queue service.
- Unit and Integration tests must be added.

### What I have used so far:
- Asp.Net Core Web API and ConsoleApp with `.Net6.0` framework.
- EntityFramworkCore as an ORM and Tools packages.
- PostgreSQL as an Database and packages.
- MongoDb for saving the archives.
- MediatR library for CQRS design pattern.
- FluentValidation for validating user inputs.
- JWT Bearer Authentication library to generate tokens.
- Identity Framework for users.
- RabbitMQ server and packages. For more info about installing on windows check the [link!](https://www.rabbitmq.com/install-windows.html)
- Redis Stack Exchange caching. For windows you can check this [guide!](https://redis.io/docs/getting-started/installation/install-redis-on-windows/)
- NLog for logging the exceptions.
- AutoMapper for mapping results.
- xUnit for Unit testing and Integration testings.
- Moq for mocking services, FluentAssertions for validating Integration test results.
- Postman and Swagger used for tests.

## Installation and Usage

- To get the project :
```
    git clone https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject.git
```
- To create the database first, in the `ShoppingList.Infrastructure` folder :
```c
    update-database
```
- To start the project, **MULTIPLE STARTUP** must be selected with `ShoppingList.Server` and `ShoppingList.Client` and `ShoppingList.Consumer` or type in those folders:
```c
    dotnet run
```
- "***dotnet restore***" command can be used to restore dependencies and tools inside the project.
-  The project can be directly started in VisualStudio by pressing '***F5***'.
- The port will be listenin on : https://localhost:5070

## Folder Structure
- Clean Architecutre design principles used in the project structure. 
- ***Core*** => will contain the base of the project which includes *Application* Interfaces, Services, and *Domain* entities.
- ***Infrastructure*** => will be communicating with databases and interface implementations.
- ***Web*** => will be responsible the presentation of the project to outer world via APIs.
- *Server* will be routing the APIs and be based server of project. *Client* will be including Blazor App for frontend users.
- ***Consumer*** => will be responsible to listen RabbitMq and save datas into Mongo Db.
- ***Tests*** => will be testing the selected methods and flow diagrams of project.

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/folders.PNG" alt="folders"/>

## Project Structure

- APIs sending a request. Then the requests are coming to MediatR.
- MediatR routing the requests related command or queries. 
- Command and Queries calling the related service to do the actual job.
- Services calling the repositories which has only access to database.
- When user compelete the list, it will be sended to Mongodb via Rabbitmq.
- Detailed project overview can be seen below.

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/project.PNG" alt="project" />
 
## Database Design

- To create a list, a category and user must be exist and selected.
- To add items, a list must be exist. Items can have different units.
- For doing all above, a user must have credentials and roles to be able to make actions.
- Relationships can be seen from the picture below.

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/database.PNG" alt="database"/>

## Logging
- Nlog has been used for logging and saving logs in an external file.
- Logs generated by Global Exception Handler. If the validations or any error occurs, that error can be shown to user in the console or can be saved in a text file in predefined location.

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/log.PNG" alt="log"/>

## Tests
- For unit testing, list and cateogry repositories tested.
- For integration testing, end points tested with an inmemory database.
- Also, a fake jwt token generated a fake user in the testing.

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/tests.PNG" alt="tests"/>


## **Bonus Blazor Client app
- Blazor Server App used to show categories, units and lists.
- Users can able to sign up and login their accounts. Also, logout when they want.
- System automatically catch the token and let the users get their lists.
- Each list can be able to seen by its own users. Besides, Admin user can see everylists.
- Screen images can be seen below.


|           Signup              | Login                                                   
|-------------------------------|-------------------------------
|<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/signup.PNG" alt="signup"/>   |<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/login.PNG" alt="login"/>       
---
     
|           Index               | Lists                                                   
|-------------------------------|-------------------------------
|<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/index.PNG" alt="index"/>                         |<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/list.PNG" alt="list"/>         
---

|           Category            |   Unit                                                   
|-------------------------------|-------------------------------   
|<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/category.PNG" alt="category"/>                       |<img src="https://github.com/186-Teleperformans-Net-Bootcamp/MuhammetAliDemir-TP.ShoppingList-FinalProject/blob/main/images/uom.PNG" alt="unit"/>

---