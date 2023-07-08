using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Common.Interfaces
{
    /// <summary>
    /// Exposes methods to interact with the Prime Number data store for the application
    /// </summary>
    public interface IPrimeNumberRepository
    {
        Task AddPrimeNumberAsync(long primeNumber);
        Task<List<long>> GetTopTenPrimeNumbersAsync();
    }
}
