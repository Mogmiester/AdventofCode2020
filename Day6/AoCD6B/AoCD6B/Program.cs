using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace AoCD6B
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day6A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string[] initialSplit = new string[1];
            initialSplit[0] = "\n\n";
            string[] customsFormsAllGroups = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);

            int runningTotalOfAnswers = 0;

            foreach (string customFormsAnswers in customsFormsAllGroups)
            {
                HashSet<Char> GroupAnswers = new HashSet<char>(alphabet.ToCharArray());
                string[] groupAnswers = customFormsAnswers.Split('\n');

                foreach (string personYesAnswers in groupAnswers)
                {
                    char[] alphabetCharArray = alphabet.ToCharArray();
                    HashSet<char> personNoAnswers = new HashSet<char>(alphabetCharArray);
                    HashSet<char> personYesAnswersHashSet = new HashSet<char>(personYesAnswers.ToCharArray());
                    personNoAnswers.ExceptWith(personYesAnswersHashSet);
                    GroupAnswers.ExceptWith(personNoAnswers);
                }

                runningTotalOfAnswers += GroupAnswers.Count;
            }

            Console.WriteLine(runningTotalOfAnswers.ToString());

        }
    }
}