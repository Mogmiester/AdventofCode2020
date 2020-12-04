using System;
using System.IO;
using System.Linq;

namespace AoCD2B
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day2A.txt";
            int linesInFile = File.ReadLines(InputFileLocation + InputFileName).Count();
            string[] PasswordRules = new string[linesInFile];
            string[] PasswordRequiredCharacter = new string[linesInFile];
            string[] Password = new string[linesInFile];
            int[] PasswordFirstCharacterPosition = new int[linesInFile];
            int[] PasswordSecondCharacterPosition = new int[linesInFile];
            int i = 0;
            int ValidPasswords = 0;
            string line;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(InputFileLocation + InputFileName);

            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split(" ");
                PasswordRules[i] = splitLine[0];
                PasswordRequiredCharacter[i] = splitLine[1];
                Password[i] = splitLine[2];
                string[] splitRules = PasswordRules[i].Split("-");
                int.TryParse(splitRules[0], out PasswordFirstCharacterPosition[i]);
                int.TryParse(splitRules[1], out PasswordSecondCharacterPosition[i]);
                i++;
            }

            for (i = 0; i < linesInFile; i++)
            {
                char requiredCharacterToCheck = PasswordRequiredCharacter[i][0];

                if (Password[i][PasswordFirstCharacterPosition[i] - 1] == requiredCharacterToCheck ^ Password[i][PasswordSecondCharacterPosition[i] - 1] == requiredCharacterToCheck)
                {
                    ValidPasswords++;
                }
            }

            Console.WriteLine(ValidPasswords.ToString());
        }
    }
}
