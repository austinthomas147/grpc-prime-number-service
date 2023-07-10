using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Client.Utilities
{
    /// <summary>
    /// Utility methods related to message requests
    /// </summary>
    public static class RequestUtility
    {
        private static int _requestId = 0;

        /// <summary>
        /// Returns the next incremented requestId to keep track of requests on the client side
        /// </summary>
        /// <returns></returns>
        public static int GetNextRequestId()
        {
            return _requestId++;
        }
    }
}
