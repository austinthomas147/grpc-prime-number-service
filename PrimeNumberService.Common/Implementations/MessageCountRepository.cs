using PrimeNumberService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberService.Common.Implementations
{
    public class MessageCountRepository : IMessageCountRepository
    {
        private int _messageCount;

        public MessageCountRepository()
        {
            _messageCount = 0;
        }

        public Task AddMessage()
        {
            _messageCount++;
            return Task.CompletedTask;
        }

        public Task<int> GetMessageCountAsync() => Task.FromResult(_messageCount);
    }
}
