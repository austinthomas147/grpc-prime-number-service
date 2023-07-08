using PrimeNumberService.Server.Utilities;

namespace PrimeNumberService.Tests.Unit
{
    [TestClass]
    public class PrimeNumberHelperUnitTests
    {
        [TestMethod]
        public void IsPrimeNumber_NegativeNumberPassedIn_ReturnsFalse()
        {
            long number = -200;

            bool isPrimeNumber = PrimeNumberHelper.IsPrimeNumber(number);

            Assert.IsFalse(isPrimeNumber);
        }

        [TestMethod]
        public void IsPrimeNumber_KnownPrimeNumberPassedIn_ReturnsTrue()
        {
            long number = 2;

            bool isPrimeNumber = PrimeNumberHelper.IsPrimeNumber(number);

            Assert.IsTrue(isPrimeNumber);
        }

        [TestMethod]
        public void IsPrimeNumber_NumberDivisibleByTwoPassedIn_ReturnsFalse()
        {
            long number = Random.Shared.NextInt64() * 2;

            bool isPrimeNumber = PrimeNumberHelper.IsPrimeNumber(number);

            Assert.IsFalse(isPrimeNumber);
        }

        [TestMethod]
        public void IsPrimeNumber_NumberDivisibleByFivePassedIn_ReturnsFalse()
        {
            long number = Random.Shared.NextInt64() * 5;

            bool isPrimeNumber = PrimeNumberHelper.IsPrimeNumber(number);

            Assert.IsFalse(isPrimeNumber);
        }

        [TestMethod]
        public void IsPrimeNumber_LargePrimeNumberPassedIn_ReturnsTrue()
        {
            long number = 60647;

            bool isPrimeNumber = PrimeNumberHelper.IsPrimeNumber(number);

            Assert.IsTrue(isPrimeNumber);
        }
    }
}