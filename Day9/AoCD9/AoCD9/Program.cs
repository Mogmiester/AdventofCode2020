using System;
using System.IO;

namespace AoCD9
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day9A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] allNumbersAsString = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            long[] allNumbers = new long[allNumbersAsString.Length];
            int preambleSize = 25;
            long weakNumber = 0;

            for (int i = 0; i < allNumbers.Length; i++)
            {
                Int64.TryParse(allNumbersAsString[i], out allNumbers[i]);
            }


            for (long i = preambleSize; i < allNumbers.Length; i++)
            {
                bool targetFound = false;

                for (long j = i - preambleSize; j < i; j++)
                {
                    for (long k = i - preambleSize; k < i; k++)
                    {
                        if (k == j) { continue; }
                        if ((allNumbers[j] + allNumbers[k]) == allNumbers[i]) { targetFound = true; }
                    }
                }

                if (!targetFound)
                {
                    Console.WriteLine(allNumbers[i].ToString());
                    weakNumber = allNumbers[i];
                    break;
                }

            }

            int windowSize = 2;
            int windowStart = 0;
            long runningTotal = allNumbers[0] + allNumbers[1];
            bool continueLooping = true;


            while (continueLooping)
            {
                if (runningTotal == weakNumber)
                {
                    long max = int.MinValue;
                    long min = int.MaxValue;

                    for (int i = windowStart; i < windowStart + windowSize; i++)
                    {
                        if (allNumbers[i] > max) { max = allNumbers[i]; }
                        if (allNumbers[i] < min) { min = allNumbers[i]; }
                    }
                    // get max and min and add
                    continueLooping = false;
                    Console.WriteLine((max + min).ToString());

                }
                else if (runningTotal < weakNumber)
                {
                    windowSize++;
                    runningTotal += allNumbers[windowSize + windowStart - 1];
                }
                else if (runningTotal > weakNumber)
                {
                    windowSize--;
                    runningTotal -= allNumbers[windowStart];
                    windowStart++;
                }
                else
                {
                    Console.WriteLine("Panic");
                    continueLooping = false;
                    //panic!!!!
                }
            }

        }
    }
}
