# GraphQL Demo

This is the project demo for the GraphQL presentation. It is a simple chatroom using GraphQL subscriptions and mutations.

The project playground also supports queries with pagination, filtering and sorting for demonstration purposes.

## How to set it up
### Server
#### Creating SQL Server
- Use docker compose to set up the server
```
docker-compose up -d
```
#### Create DB migration
- First install Dotnet EF globally if it is not already installed
```
dotnet tool install --global dotnet-ef
```

- Apply the latest migration
```
dotnet ef database update
```

#### Spool up the Server
- Run the server like any other dotnet app
```
dotnet run
```

### Client

#### Install Node Modules
- First open up a separate console, then `cd` into `Client/chatbox-client`
```
cd Client && cd chatbox-client
```
- Install modules
```
npm install
```

#### Start the client
- Simply run the npm script
```
npm run start
```


## Presentation Link

[Google Slides Link for the presentation](https://docs.google.com/presentation/d/1LSr2vCpBZ4nGeTCa9hvEK_uZjWafEx100cHeTV5iTLk/edit?usp=sharing)