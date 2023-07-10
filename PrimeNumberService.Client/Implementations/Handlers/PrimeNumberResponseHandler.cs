using Microsoft.Extensions.Logging;
using PrimeNumberService.Client.Exceptions;
using PrimeNumberService.Client.Interfaces.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Client.Implementations.Handlers
{
    public class PrimeNumberResponseHandler : IPrimeNumberResponseHandler
    {
        private readonly ILogger<PrimeNumberResponseHandler> _logger;
        private readonly int _expectedRequestCount;

        public PrimeNumberResponseHandler(ILogger<PrimeNumberResponseHandler> logger,
                                          int expectedResponseCount) 
        {
            _logger = logger;
            _expectedRequestCount = expectedResponseCount;
        }

        public Task HandlePrimeNumberResponsesAsync(List<PrimeNumberResponse> primeNumberResponses)
        {
            if (primeNumberResponses.Count != _expectedRequestCount)
                throw new MissingResponseException($"Expected {_expectedRequestCount} responses, got {primeNumberResponses.Count}");

            primeNumberResponses.ForEach(response =>
            {
                var roundTripTimeInSeconds = DateTime.Now - new DateTime(response.IncomingTimestamp);

                _logger.LogInformation($"Total round trip time for requestId: {response.RequestId} - {roundTripTimeInSeconds.TotalSeconds} seconds...");
            });

            return Task.CompletedTask;
        }
    }
}
