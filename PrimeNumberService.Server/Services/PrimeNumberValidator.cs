using Grpc.Core;
using PrimeNumberService.Common.Interfaces;
using PrimeNumberService.Server.Protos;
using PrimeNumberService.Server.Utilities;

namespace PrimeNumberService.Server.Services
{
    public class PrimeNumberValidator : Protos.PrimeNumberValidator.PrimeNumberValidatorBase
    {
        private readonly ILogger<PrimeNumberValidator> _logger;
        private readonly IPrimeNumberRepository _primeNumberRepository;
        private readonly IMessageCountRepository _messageCountRepository;

        public PrimeNumberValidator(ILogger<PrimeNumberValidator> logger,
                                    IPrimeNumberRepository primeNumberRepository,
                                    IMessageCountRepository messageCountRepository)
        {
            _logger = logger;
            _primeNumberRepository = primeNumberRepository;
            _messageCountRepository = messageCountRepository;
        }

        public override async Task<PrimeNumberResponse> CheckPrimeNumber(PrimeNumberRequest request, 
                                                                   ServerCallContext context)
        {
            var response = await BuildPrimeNumberResponseAsync(request);

            await _messageCountRepository.AddMessage();

            return response;
        }

        public override async Task<MultiPrimeNumberResponse> CheckPrimeNumberStream(IAsyncStreamReader<PrimeNumberRequest> requestStream, 
                                                                                    ServerCallContext context)
        {
            var responses = new MultiPrimeNumberResponse();

            await foreach(var request in requestStream.ReadAllAsync())
            {
                var response = await BuildPrimeNumberResponseAsync(request);
                responses.Response.Add(response);

                await _messageCountRepository.AddMessage();
            }

            return responses;
        }

        private async Task<PrimeNumberResponse> BuildPrimeNumberResponseAsync(PrimeNumberRequest request)
        {
            var response = new PrimeNumberResponse();

            response.RequestId = request.Id;
            response.IsPrimeNumber = PrimeNumberHelper.IsPrimeNumber(request.Number);
            response.Number = request.Number;
            response.IncomingTimestamp = request.Timestamp;

            if (response.IsPrimeNumber)
                await _primeNumberRepository.AddPrimeNumberAsync(response.Number);

            return response;
        }
    }
}
