using PrimeNumberService.Common.Interfaces;

namespace PrimeNumberService.Server.HostedServices
{
    public class MessageStatisticsBackgroundService : BackgroundService
    {
        private readonly ILogger<MessageStatisticsBackgroundService> _logger;
        private readonly IPrimeNumberRepository _primeNumberRepository;
        private readonly IMessageCountRepository _messageCountRepository;

        public MessageStatisticsBackgroundService(ILogger<MessageStatisticsBackgroundService> logger, 
                                                  IPrimeNumberRepository primeNumberRepository,
                                                  IMessageCountRepository messageCountRepository)
        {
            _logger = logger;
            _primeNumberRepository = primeNumberRepository;
            _messageCountRepository = messageCountRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWorkAsync();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWorkAsync();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Task was cancelled...");
            }
            
        }

        private async Task DoWorkAsync()
        {
            _logger.LogInformation("Getting gRPC Prime Number statistics...");
            var topTenPrimeNumbers = await _primeNumberRepository.GetTopTenPrimeNumbersAsync();

            if (topTenPrimeNumbers.Any())
            {
                for (int i = 0; i < topTenPrimeNumbers.Count; i++)
                {
                    _logger.LogInformation($"#{i + 1}: {topTenPrimeNumbers[i]}");
                }
            }
            else
                _logger.LogInformation("No prime numbers currently saved...");

            var messageCount = await _messageCountRepository.GetMessageCountAsync();
            _logger.LogInformation($"Received {messageCount} messages in server lifespan...");
        }
    }
}
