using System;
using System.IO;
using System.Linq;

namespace LabLibrary
{
    public static class Lab1
    {
        public static void Run(string inputFile, string outputFile)
        {
            var input = File.ReadAllLines(inputFile);
            int n = int.Parse(input[0].Split(' ')[0]);
            int k = int.Parse(input[0].Split(' ')[1]);
            int[] arrangement = input[1].Split(' ').Select(int.Parse).ToArray();

            if (arrangement.Length != k)
                throw new InvalidOperationException("Arrangement length does not match k.");

            long position = CalculatePosition(n, k, arrangement);
            File.WriteAllText(outputFile, position.ToString());
        }

        private static long Factorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private static long CalculatePosition(int n, int k, int[] arrangement)
        {
            if (arrangement == null || arrangement.Distinct().Count() != arrangement.Length)
                throw new InvalidOperationException("Duplicate values in arrangement are not allowed or arrangement is null.");

            if (arrangement.Any(x => x < 1 || x > n))
                throw new InvalidOperationException("All values in arrangement must be between 1 and n.");

            bool[] used = new bool[n + 1];
            long position = 1;

            for (int i = 0; i < k; i++)
            {
                int current = arrangement[i];
                for (int j = 1; j < current; j++)
                {
                    if (!used[j])
                    {
                        position += Factorial(n - i - 1);
                    }
                }
                used[current] = true;
            }

            return position;
        }
    }
}