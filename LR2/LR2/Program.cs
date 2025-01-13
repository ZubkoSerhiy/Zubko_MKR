using System;
using System.IO;
using System.Linq;

public class Program
{
    public static long ProcessData(int n, string[] input)
    {
        if (n <= 0 || input == null || input.Length <= 1)
        {
            throw new ArgumentException("Input data is invalid.");
        }

        int[] times = new int[n];

        for (int i = 0; i < n; i++)
        {
            string[] parts = input[i + 1].Split(':');
            if (parts.Length != 3 || !parts.All(p => int.TryParse(p, out _)))
            {
                parts = new string[] { "0", "0", "0" };
            }

            int h = int.Parse(parts[0]), m = int.Parse(parts[1]), s = int.Parse(parts[2]);

            h = h % 24;
            m = m % 60;
            s = s % 60;

            times[i] = (h * 3600 + m * 60 + s) % 43200;
        }

        int[] freq = new int[43200];
        foreach (var time in times)
        {
            if (time < 43200)
            {
                freq[time]++;
            }
        }

        long[] cumulative = new long[43200];
        for (int t = 1; t < 43200; t++)
        {
            cumulative[t] = cumulative[t - 1] + freq[t] * t;
        }

        long totalSum = cumulative[43199];
        long bestTime = 0, minCost = totalSum;

        for (int t = 1; t < 43200; t++)
        {
            totalSum += 2 * cumulative[t - 1] - cumulative[43199];
            if (totalSum < minCost)
            {
                minCost = totalSum;
                bestTime = t;
            }
        }

        return bestTime;
    }

    static void Main()
    {
        string inputFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "INPUT.txt");
        string outputFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "OUTPUT.txt");

        var input = File.ReadAllLines(inputFilePath);

        if (input.Length == 0 || !int.TryParse(input[0], out int n) || n <= 0)
        {
            throw new ArgumentException("Invalid input data.");
        }

        long bestTime = ProcessData(n, input);

        int hours = (int)(bestTime / 3600);
        int minutes = (int)((bestTime % 3600) / 60);
        int seconds = (int)(bestTime % 60);

        string result = $"{(hours == 0 ? 12 : hours)}:{minutes:D2}:{seconds:D2}";

        File.WriteAllText(outputFilePath, result);
    }
}
