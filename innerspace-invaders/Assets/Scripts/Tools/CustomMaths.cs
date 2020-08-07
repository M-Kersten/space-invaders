namespace Invaders.Tools
{
    public static class CustomMaths
    {
        public static int CalculateFibonacci(int n)
        {
            if (n < 1)
            {
                UnityEngine.Debug.LogError($"number to get fibonacci for is {n}, this is lower than 1 and cannot be used in calculation");
                return 0;
            }

            int fibNumber = n + 1;
            int[] fibonacci = new int[fibNumber + 1];

            fibonacci[0] = 0;
            fibonacci[1] = 1;
            for (int i = 2; i <= fibNumber; i++)
            {
                fibonacci[i] = fibonacci[i - 2] + fibonacci[i - 1];
            }
            return fibonacci[fibNumber];
        }
    }
}