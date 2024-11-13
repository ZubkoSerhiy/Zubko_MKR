using System;
using System.IO;
using System.Linq;

//Щоб зрозуміти, як визначити позицію розміщення в лексикографічному порядку, уявімо собі, що ми складаємо список всіх можливих варіантів розміщень з чисел 1 до N і ставимо їх в алфавітному порядку.
//В цьому порядку спочатку йдуть найменші числа, потім більші. Наприклад візьмемо 2 і 3, тоді для K=2 і N=3 лексикографічний порядок виглядатиме так:
//(1, 2), (1, 3), (2, 1), (2, 3), (3, 1), (3, 2)
//Тепер, щоб знайти позицію розміщення (2, 3) у цьому списку:
//Спочатку перевіряємо, скільки розміщень з першого елемента можуть йти перед (2, 3). Це (1, 2) і (1, 3), всього два варіанти.
//Тепер беремо наступний елемент розміщення (2, 3). Оскільки 3 це останнє можливе число для цього розміщення, то його позиція після (2, 1), (2, 3).
//Отже, позиція розміщення (2, 3) в списку буде 4.

public class Program
{
    public static long Factorial(int n)
    {
        long result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    public static long CalculatePosition(int n, int k, int[] arrangement)
    {
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

    public static void Main()
    {
        string inputFilePath = "INPUT.TXT";
        var input = File.ReadAllLines(inputFilePath);

        int n = int.Parse(input[0].Split(' ')[0]);
        int k = int.Parse(input[0].Split(' ')[1]);
        int[] arrangement = input[1].Split(' ').Select(int.Parse).ToArray();

        long position = CalculatePosition(n, k, arrangement);
        string outputFilePath = "OUTPUT.TXT";
        File.WriteAllText(outputFilePath, position.ToString());
    }
}
