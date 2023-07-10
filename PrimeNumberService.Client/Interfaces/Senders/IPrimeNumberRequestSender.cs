using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Client.Interfaces.Senders
{
    /// <summary>
    /// The class responsible for sending the messages to the gRPC server and closing the stream
    /// </summary>
    public interface IPrimeNumberRequestSender
    {
        Task<List<PrimeNumberResponse>> SendPrimeNumberRequestsAsync();
    }
}
