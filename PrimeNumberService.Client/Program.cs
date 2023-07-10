using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrimeNumberService.Client;
using PrimeNumberService.Client.Implementations.Handlers;
using PrimeNumberService.Client.Implementations.Senders;

var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

bool isValidRequestCount = int.TryParse(configuration["RequestCount"], out int requestCount);

if (!isValidRequestCount)
    throw new ArgumentException("Invalid request count configuration passed into appsettings.json!");

string grpcServerUrl = configuration["gRPC:ServerAddress"];

if (string.IsNullOrEmpty(grpcServerUrl))
    throw new ArgumentException("Invalid server url passed in to the gRPC config!");

using var channel = GrpcChannel.ForAddress(grpcServerUrl);
var client = new PrimeNumberValidator.PrimeNumberValidatorClient(channel);
var sender = new PrimeNumberRequestSender(loggerFactory.CreateLogger<PrimeNumberRequestSender>(), client, requestCount);
var handler = new PrimeNumberResponseHandler(loggerFactory.CreateLogger<PrimeNumberResponseHandler>(), requestCount);

using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

while (await timer.WaitForNextTickAsync())
{
    var responses = await sender.SendPrimeNumberRequestsAsync();
    await handler.HandlePrimeNumberResponsesAsync(responses);
}

