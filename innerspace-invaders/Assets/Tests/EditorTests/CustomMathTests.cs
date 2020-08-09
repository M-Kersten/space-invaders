using NUnit.Framework;

namespace Tests
{
    public class CustomMathTests
    {
        [Test]
        public void Calculate_high_Fibonacci_number()
        {
            // arrange
            int fibonacciNumber = 20;


            // act
            int calculatedFibonacci = Invaders.Tools.CustomMaths.CalculateFibonacci(fibonacciNumber);

            // assert that 20 times fibonacci is 10946 as calculated on https://onlinenumbertools.com/calculate-fibonacci-numbers
            Assert.AreEqual(10946, calculatedFibonacci);
        }
    }
}
