namespace PrimeNumberService.Server.Utilities
{
    public static class PrimeNumberHelper
    {
        /// <summary>
        /// Determines whether a given integer is a prime number or not
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPrimeNumber(long number)
        {
            if (number == 2 ||
                number == 3 ||
                number == 5)
                return true;

            ///Anything less than two is not a prime number and anything divisible by 2 is not a prime number
            ///And anything divisible by 5 (ending in 5 or 0) is not a prime number
            if (number < 2 ||
                number % 2 == 0 ||
                number % 5 == 0)
                return false;

            ///Per the trial division method, we can take the square root of the number, and starting at 5, divide iteratively until
            ///we hit the square root. If anything is a whole integer, we know it is a prime number
            var trialDivisionBoundary = Math.Ceiling(Math.Sqrt(number));
            var squareRoot = Math.Sqrt(number);

            ///Starting at 7 allows us to take a few extra iterations out of the process since we already checked 
            ///for divisibility by 5 and 2, which eliminates 6
            for (int i = 7; i <= trialDivisionBoundary; i++)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
