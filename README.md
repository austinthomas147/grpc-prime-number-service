# grpc-prime-number-service

## Development Process
The first step I take in every project is to scaffold out the different projects that I need. This helps me organize the code, and ensure that my code is where it should be. This includes whatever class libraries I may be using, and the test project.

When writing the gRPC implementation, I started with the server side. First, you need to at a minimum start with the protobuf file on the server. But starting with the server also allows me to run multiple tests using a gRPC UI tool (I used [BloomRPC](https://github.com/bloomrpc/bloomrpc) which is technically archived, but I've used it many times before and had it readily available on my machine). This way I could identify any issues with the base server logic before implementing the client and starting to scale the number of messages processed through the system.

Before getting to testing the server, I had to do a little bit of research on the best way to determine what a prime number is. I found a few methods from the [Wikipedia](https://en.wikipedia.org/wiki/Prime_number). The simplest, albeit slow, method was trial division. In this method you take the square root of the number under test, and iteratively divide it by all numbers leading up to it. While it isn't the fastest method out there, it was the one that ensured there would be no errors.

Before implementing the final solution, I debated creating a utility that, on runtime of the server, would calculate all prime numbers between 2-1000 since that is the range we can expect given the problem statement of the client sending a random number between 1-1000. But that ultimately isn't scalable and the moment you have to accept numbers greater than 1000, you have to rewrite your implementation. So I went with a trial division implementation.

Since I knew the importance of determining whether a number was a prime number, I wanted to be sure the code worked in a variety of scenarios, especially given the number of if branches in the `PrimeNumberHelper`. So before actually implementing the `PrimeNumberValidator` class as part of the gRPC implementation, I wrote multiple tests to ensure I wasn't implementing faulty code.

I did contemplate putting the `PrimeNumberHelper` class into a private method in the `PrimeNumberValidator`, but that wouldn't allow me to put the `IsPrimeNumber` method under test the way I'd like, so I added it as a static utility class.

Once the unit tests were written and were all green, I wrote the implementation for the `PrimeNumberValidator`. It was probably slightly overkill to create a private response builder method. But I wanted to keep the actual gRPC overridden method as clean as possible. Once the implementation was done, I started testing the gRPC endpoint by sending messages through BloomRPC. I ran into a few small configuration issues, but those were on the BloomRPC side, not any part of my code. I ran a decent number of tests using BloomRPC to ensure I was receiving the correct, expected data back. 

Now that I have tested the server responds correctly given the parameters, I can work on the client implementation.

## Future Considerations
