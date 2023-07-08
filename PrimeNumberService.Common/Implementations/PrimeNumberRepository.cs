using PrimeNumberService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Common.Implementations
{
    public class PrimeNumberRepository : IPrimeNumberRepository
    {
        private Dictionary<long, int> _primeNumbers;
        private object _lockObject;

        public PrimeNumberRepository()
        {
            _primeNumbers = new Dictionary<long, int>();
            _lockObject = new object();
        }

        public Task AddPrimeNumberAsync(long primeNumber)
        {
            lock (_lockObject)
            {
                if (_primeNumbers.ContainsKey(primeNumber))
                    _primeNumbers[primeNumber]++;
                else
                {
                    _primeNumbers.Add(primeNumber, 1);
                }
            }

            return Task.CompletedTask;
        }

        public Task<List<long>> GetTopTenPrimeNumbersAsync()
        {
            var topTen = new List<long>();

            lock (_lockObject)
            {
                var sortedPrimeNumbers = _primeNumbers.OrderByDescending(x => x.Value);

                if (_primeNumbers.Count < 10)
                    topTen = sortedPrimeNumbers.Select(x => x.Key).ToList();
                else   
                    topTen = sortedPrimeNumbers.Take(10).Select(x => x.Key).ToList();
            }

            return Task.FromResult(topTen);
        }
    }
}
