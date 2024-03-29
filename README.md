# AspNetCore2.2-WebAPI-RabbitMQ-MongoDB-Docker
A project using Web API (Asp.Net Core 2.2), xUnit, RabbitMQ, NoSQL Database (MongoDB) and also Docker.

## For the CI (Continuous Integration) I've used the following tool:

- [GitHub Actions](https://github.com/nmaia/AspNetCore-WebAPI-RabbitMQ-MongoDB/actions)

## This project was developed using the following structure:

![Solution Architecure Image](https://github.com/nmaia/AspNetCore-WebAPI-RabbitMQ-MongoDB/blob/develop/Demo/docs/Project%20Structure/Project_Structure.png)

## Just a short story about how is the data flow on this project:

- Once you insert some research, this research will be published into the queue and then all the family tree will be published as well;
- There will be some hosted services that will consume the messages and then it will be written into the database;
- After these steps, you can access the MongoBD Express and check all the available documents.

## Requirements to run this project:

- [Asp.Net Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) (SDK or Runtime);
- [Docker Desktop](https://www.docker.com/products/docker-desktop) - to run Linux Containers;

## In order to run this project, you must go on the following steps:

- Clone the git repository;
- Open the local folder where the repository was downloaded;
- Go to the folder where you can find the `docker-compose.yml` file;
- In this folder, open the command prompt (e.g.: cmd.exe);
- Execute the following commands:
  - Build Images
    
  ```
  docker-compose build
  ```

  - Compose Containers

  ```
  docker-compose up -d
  ```

- Once you run the above command, all the images I've set up in the `docker-compose.yml` file will start their own containers.

- In order to check the running containers you can run the following command:

  ```
  docker container ps
  ```
  
- If you want to check what port the API is using, you can run the following command:

  ```
  docker-compose ps
  ```
  
- If the API doesn't open automatically on the browser, you can use the above command to get the port is being used for the API and access the API with this URL:

  - `http://localhost:<port>/swagger/index.html`: where `<port>` will be the port listed by the command `docker-compose ps`.
  - **Important!** - You have to use the `HTTP` port that points to port `80`.

## After running the containers, you should have the following environment:

- The API will open a new tab using your default web browser and the swagger page will be available to test all the endpoints.
  - You'll need to register some data before using the GET endpoints, so...
  - Firstly, you should register a research using the endpoing `/api/researches/insert-one`
  - Now you can go and test the other endpoints!

- RabbitMQ:
  - URL: `http://localhost:15672/`
  - User: **user**
  - Password: **pass**
  - Virtual-Host: challenge-dev
- Mongo
  - Port: 27017
- MongoDB Express
  - URL: `http://localhost:8081/`
  - User: **user**
  - Password **user123**

I hope you enjoy the code.
