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
            int stringCounter = 0;
            bool debugMode = false;

            foreach (string line in fileLines)
            {
                stringCounter++;
                if (line.Length == 10)
                {
                    int binaryStringToNumber = BinaryStringToNumber(line.Substring(0, 7), "F", "B") * 8 + BinaryStringToNumber(line.Substring(7, 3), "L", "R");
                    if (debugMode) { Console.WriteLine(binaryStringToNumber.ToString()); }
                    currentMax = Math.Max(currentMax, binaryStringToNumber);
                }
                else
                {
                    Console.WriteLine(@"Error with line: " + stringCounter.ToString());
                }
            }

            Console.WriteLine(currentMax.ToString());
        }

        public static int BinaryStringToNumber(string inputString, string zeroString, string oneString)
        {
            return Convert.ToInt32(inputString.Replace(zeroString, "0").Replace(oneString, "1"), 2);
        }
    }
}