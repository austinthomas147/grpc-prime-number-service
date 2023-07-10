using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Client.Interfaces.Handlers
{
    /// <summary>
    /// Handles the responses for messages returned from the gRPC server
    /// </summary>
    public interface IPrimeNumberResponseHandler
    {
        Task HandlePrimeNumberResponsesAsync(List<PrimeNumberResponse> primeNumberResponses);
    }
}
