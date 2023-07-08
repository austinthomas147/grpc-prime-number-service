using Grpc.Core;
using PrimeNumberService.Server.Protos;
using PrimeNumberService.Server.Utilities;

namespace PrimeNumberService.Server.Services
{
    public class PrimeNumberValidator : Protos.PrimeNumberValidator.PrimeNumberValidatorBase
    {
        private readonly ILogger<PrimeNumberValidator> _logger;

        public PrimeNumberValidator(ILogger<PrimeNumberValidator> logger)
        {
            _logger = logger;
        }

        public override Task<PrimeNumberResponse> CheckPrimeNumber(PrimeNumberRequest request, 
                                                                   ServerCallContext context)
        {
            var response = BuildPrimeNumberResponse(request);

            return Task.FromResult(response);
        }

        private PrimeNumberResponse BuildPrimeNumberResponse(PrimeNumberRequest request)
        {
            var response = new PrimeNumberResponse();

            response.RequestId = request.Id;
            response.IsPrimeNumber = PrimeNumberHelper.IsPrimeNumber(request.Number);
            response.Number = request.Number;
            response.IncomingTimestamp = request.Timestamp;

            return response;
        }
    }
}
