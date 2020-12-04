using System;
using System.IO;
using System.Linq;

namespace AoCD2A
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
            int[] PasswordLowerReqCharLimit = new int[linesInFile];
            int[] PasswordUpperReqCharLimit = new int[linesInFile];
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
                int.TryParse(splitRules[0], out PasswordLowerReqCharLimit[i]);
                int.TryParse(splitRules[1], out PasswordUpperReqCharLimit[i]);
                i++;
            }

            for (i = 0; i < linesInFile; i++)
            { 
                char requiredCharacterToCheck = PasswordRequiredCharacter[i][0];
                int requiredCharacterOccurences = 0;
                for (int j = 0; j < Password[i].Length; j++)
                {
                    if (Password[i][j] == requiredCharacterToCheck)
                    {
                        requiredCharacterOccurences++;
                    }
                }
                    if (requiredCharacterOccurences >= PasswordLowerReqCharLimit[i] && requiredCharacterOccurences <= PasswordUpperReqCharLimit[i])
                    {
                        ValidPasswords++;
                    }
                }

            Console.WriteLine(ValidPasswords.ToString());
        }
    }
}
