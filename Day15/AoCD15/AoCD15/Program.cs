using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD15
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day15A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "," };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            long turnNumber = 0;
            long lastNumber = 0;
            Dictionary<long, long> whenNumbersWerePlayed = new Dictionary<long, long>();

            foreach (string intialNumber in splitFile)
            {
                turnNumber++;
                if (!(turnNumber == 1)) { whenNumbersWerePlayed[lastNumber] = turnNumber - 1; };
                Int64.TryParse(intialNumber, out lastNumber);
            }

            while (turnNumber < 30000000)
            {
                turnNumber++;
                if (whenNumbersWerePlayed.TryGetValue(lastNumber, out long timeLastPlayed))
                {
                    whenNumbersWerePlayed[lastNumber] = turnNumber - 1;
                    lastNumber = turnNumber - 1 - timeLastPlayed;
                }
                else
                {
                    whenNumbersWerePlayed[lastNumber] = turnNumber - 1;
                    lastNumber = 0;
                }
                if (turnNumber == 2020)
                {
                    Console.WriteLine(lastNumber.ToString());
                }
            }
            Console.WriteLine(lastNumber.ToString());
        }
    }
}
