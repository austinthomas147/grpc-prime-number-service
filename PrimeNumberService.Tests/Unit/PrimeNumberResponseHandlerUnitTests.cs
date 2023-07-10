using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using PrimeNumberService.Client;
using PrimeNumberService.Client.Exceptions;
using PrimeNumberService.Client.Implementations.Handlers;
using PrimeNumberService.Client.Interfaces.Handlers;

namespace PrimeNumberService.Tests.Unit
{
    [TestClass]
    public class PrimeNumberResponseHandlerUnitTests
    {
        private ILogger<PrimeNumberResponseHandler> _logger;
        private int _requestCount = 10;
        private IPrimeNumberResponseHandler _handler;

        private Fixture _fixture =  new Fixture();

        [TestInitialize]
        public void Init()
        {
            _fixture = new Fixture();
            _logger = Mock.Of<ILogger<PrimeNumberResponseHandler>>();
            _handler = new PrimeNumberResponseHandler(_logger, _requestCount);
        }

        [TestMethod]
        public async Task HandleResponsesAsync_LessThanRequestedCountResponsesPassedIn_ThrowsException()
        {
            var responses = _fixture.CreateMany<PrimeNumberResponse>(_requestCount - 1).ToList();

            await Assert.ThrowsExceptionAsync<MissingResponseException>(async () =>
                                                                  await _handler.HandlePrimeNumberResponsesAsync(responses));
        }

        [TestMethod]
        public async Task HandleResponsesAsync_RequestedCountResponsesPassedIn_DoesNotThrowException()
        {
            var responses = _fixture.CreateMany<PrimeNumberResponse>(_requestCount).ToList();

            await _handler.HandlePrimeNumberResponsesAsync(responses);
        }
    }
}
