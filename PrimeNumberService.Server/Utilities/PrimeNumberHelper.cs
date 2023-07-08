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
            ///Anything less than 2 we know is not a prime number
            if (number < 2)
                return false;

            ///Per the trial division method, we can take the square root of the number divide iteratively until
            ///we hit the square root. If anything is a whole integer, we know it is a prime number
            var squareRoot = Math.Sqrt(number);

            ///Starting at 2 since we know 2 is the first primary number
            for (long i = 2; i <= squareRoot; i++)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
