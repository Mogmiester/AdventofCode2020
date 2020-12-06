using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace AoCD6A
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day6A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");

            string[] initialSplit = new string[1];
            initialSplit[0] = "\n\n";
            string[] customsFormsAllGroups = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);

            HashSet<HashSet<char>> GroupAnswers = new HashSet<HashSet<char>>();

            int runningTotalOfAnswers = 0;

            foreach (string customFormsAnswers in customsFormsAllGroups)
            {
                HashSet<char> uniqueAnswers = new HashSet<char>(customFormsAnswers.Replace("\n", "").ToCharArray());

                GroupAnswers.Add(uniqueAnswers);
                runningTotalOfAnswers += uniqueAnswers.Count;
            }

            Console.WriteLine(runningTotalOfAnswers.ToString());

        }
    }
}
