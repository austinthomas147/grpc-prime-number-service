using Microsoft.Extensions.Logging;
using PrimeNumberService.Client.Interfaces.Senders;
using PrimeNumberService.Client;
using Microsoft.Extensions.Configuration;
using PrimeNumberService.Client.Utilities;

namespace PrimeNumberService.Client.Implementations.Senders
{
    public class PrimeNumberRequestSender : IPrimeNumberRequestSender
    {
        private readonly ILogger<PrimeNumberRequestSender> _logger;
        private readonly PrimeNumberValidator.PrimeNumberValidatorClient _client;
        private readonly int _requestCount;
        
        public PrimeNumberRequestSender(ILogger<PrimeNumberRequestSender> logger,
                                        PrimeNumberValidator.PrimeNumberValidatorClient client,
                                        int requestCount)
        {
            _logger = logger;
            _client = client;
            _requestCount = requestCount;
        }

        public async Task<List<PrimeNumberResponse>> SendPrimeNumberRequestsAsync()
        {
            var call = _client.CheckPrimeNumberStream();
            var random = new Random();
            
            for (int i = 0; i < _requestCount; i++)
            {
                var request = BuildNewPrimeNumberRequest(random);

                await call.RequestStream.WriteAsync(request);
            }

            await call.RequestStream.CompleteAsync();

            var response = await call;

            return response.Response.ToList();
        }

        private PrimeNumberRequest BuildNewPrimeNumberRequest(Random random)
        {
            var request = new PrimeNumberRequest();

            request.Number = random.Next(1, 10000);
            request.Timestamp = DateTime.Now.Ticks;
            request.Id = RequestUtility.GetNextRequestId();

            return request;
        }
    }
}
