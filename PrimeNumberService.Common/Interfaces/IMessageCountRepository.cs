using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Common.Interfaces
{
    public interface IMessageCountRepository
    {
        Task AddMessage();
        Task<int> GetMessageCountAsync();
    }
}
