using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AoCD5B
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day5A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] fileLines = fileAsString.Split('\n');
            int currentMax = -1;
            int currentMin = 999;
            int stringCounter = 0;
            bool debugMode = false;
            bool[] seatTaken = new bool[1024];


            foreach (string line in fileLines)
            {
                stringCounter++;
                if (line.Length == 10)
                {
                    int binaryStringToNumber = BinaryStringToNumber(line.Substring(0, 7), "F", "B") * 8 + BinaryStringToNumber(line.Substring(7, 3), "L", "R");
                    if (debugMode) { Console.WriteLine(binaryStringToNumber.ToString()); }
                    seatTaken[binaryStringToNumber] = true;
                    currentMax = Math.Max(currentMax, binaryStringToNumber);
                    currentMin = Math.Min(currentMin, binaryStringToNumber);
                }
                else
                {
                    Console.WriteLine(@"Error with line: " + stringCounter.ToString());
                }
            }

            for (int i = currentMin; i < currentMax; i++)
            {
                if (!seatTaken[i]) { Console.WriteLine(i.ToString()); }
            }
        }

        public static int BinaryStringToNumber(string inputString, string zeroString, string oneString)
        {
            return Convert.ToInt32(inputString.Replace(zeroString, "0").Replace(oneString, "1"), 2);
        }
    }
}