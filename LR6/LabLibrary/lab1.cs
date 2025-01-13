namespace LabLibrary
{
    public static class Lab1
    {
        public static string Run(string[] lines)
        {
            int n = int.Parse(lines[0].Split(' ')[0]);
            int k = int.Parse(lines[0].Split(' ')[1]);
            int[] arrangement = lines[1].Split(' ').Select(int.Parse).ToArray();

            if (arrangement.Length != k)
                throw new InvalidOperationException("Arrangement length does not match k.");

            long result = CalculatePosition(n, k, arrangement);
            return result.ToString();
        }

        private static long Factorial(int n)
        {
            long fact = 1;
            for (int i = 1; i <= n; i++)
            {
                fact *= i;
            }
            return fact;
        }

        private static long CalculatePosition(int n, int k, int[] arrangement)
        {
            if (arrangement == null || arrangement.Distinct().Count() != arrangement.Length)
                throw new InvalidOperationException("Duplicate values in arrangement are not allowed or arrangement is null.");

            if (arrangement.Any(x => x < 1 || x > n))
                throw new InvalidOperationException("All values in arrangement must be between 1 and n.");

            bool[] used = new bool[n + 1];
            long result = 1;

            for (int i = 0; i < k; i++)
            {
                int current = arrangement[i];
                for (int j = 1; j < current; j++)
                {
                    if (!used[j])
                    {
                        result += Factorial(n - i - 1);
                    }
                }
                used[current] = true;
            }

            return result;
        }
    }
}
