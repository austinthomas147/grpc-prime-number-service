# gRPC Prime Number Client and Server

## Description
This project is a gRPC client and server implementation of a prime number service. The server accepts a message with the following format:
```
message PrimeNumberRequest {
  int64 id = 1;
  int64 number = 2;
  int64 timestamp = 3;
}
```

The server will determine if the number is a prime number, using the trial division method, and return the following response to the client:
```
message PrimeNumberResponse {
  int64 requestId = 1;
  int64 number = 2;
  int64 incomingTimestamp = 3;
  bool isPrimeNumber = 4;
}
```

The server will also keep track of basic statistics like:
- How many total messages the server has received
- How many times a distinct prime number has been sent to the system

It will print out the top 10 prime numbers received every second, along with the total number of messages received.

The client sends a configurable number of messages to the server and handles the responses for each message. Upon response, the client will verify that all messages sent had a response, and will throw an exception if it finds a missing response. The client will also log the round trip time for each request, from client -> server -> client.

## How to run this project
The Makefile can be used to build, test, and publish the application. `publish` will put the self-contained executables into the `build` folder in the root of the repository. In order to run the application, verify that the `Endpoints` specificed in the `Kestrel` section of the `appsettings.json` file for the server are valid for your current configuration, and run the server executable.

Once the server executable has successfully started up, you can configure the gRPC server url in the `appsettings.json` file for the client, as well as the number of requests you'd like to send per second. Run the client executable and watch the console logs for both applications to see the work they are both doing.


