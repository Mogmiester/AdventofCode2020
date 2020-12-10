using System;
using System.IO;

namespace AoCD10
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day10A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] allNumbersAsString = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            long[] allNumbers = new long[allNumbersAsString.Length];
            long[] numberOfPaths = new long[allNumbers.Length];
            int[] differences = new int[3];

            numberOfPaths[numberOfPaths.Length - 1] = 1;
            long numberOfValidPaths = 0;

            for (int i = 0; i < allNumbers.Length; i++)
            {
                Int64.TryParse(allNumbersAsString[i], out allNumbers[i]);
            }

            Array.Sort(allNumbers);

            for (int i = 1; i < allNumbers.Length; i++)
            {
                long difference = allNumbers[i] - allNumbers[i - 1];

                if(difference > 3 || difference < 1)
                {
                    Console.WriteLine("Panic!");
                    break;
                }
                differences[difference - 1]++;
            }

            Console.WriteLine(((differences[0] + 1) * (differences[2] + 1)).ToString());

            for (int i = numberOfPaths.Length - 2; i >= 0; i--)
            {
                for (int j = 1; j <= Math.Min(3, numberOfPaths.Length - i - 1); j++)
                {
                    if ((allNumbers[i + j] - allNumbers[i]) <= 3)
                    {
                        numberOfPaths[i] += numberOfPaths[i + j];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if(allNumbers[i] <= 3)
                {
                    numberOfValidPaths += numberOfPaths[i];
                }
            }

            Console.WriteLine(numberOfValidPaths.ToString());

        }
    }
}
