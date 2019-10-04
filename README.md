# SignalRSample
A Sample Project where a two clients communicate per SignalR via a WebServer and data is persisted in a MongoDb. Everything is automatically set up with docker-compose

## Setup

To run everything open three terminals.

**Terminal 1**
```
docker-compose up
```

**Terminal 2**
```
cd ./TheReceiverClient
dotnet run
```

**Terminal 3**
```
cd ./TheSenderClient
dotnet run
```
