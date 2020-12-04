using System;
using System.IO;
using System.Linq;

namespace AoCD3B
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day3A.txt";
            int linesInFile = File.ReadLines(InputFileLocation + InputFileName).Count();
            string[] pisteLines = new string[linesInFile];
            char tree = '#';
            int i = 0;
            int xMax = 0;
            long multiplicationResult = 1;
            string line;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(InputFileLocation + InputFileName);

            while ((line = file.ReadLine()) != null)
            {
                pisteLines[i] = line;
                i++;
            }

            xMax = pisteLines[0].Length;

            int[] xMovementOptions = new int[5];
            xMovementOptions[0] = 1;
            xMovementOptions[1] = 3;
            xMovementOptions[2] = 5;
            xMovementOptions[3] = 7;
            xMovementOptions[4] = 1;
            int[] yMovementOptions = new int[5];
            yMovementOptions[0] = 1;
            yMovementOptions[1] = 1;
            yMovementOptions[2] = 1;
            yMovementOptions[3] = 1;
            yMovementOptions[4] = 2;


            for (int j = 0; j < 5; j++)
            {

                int xCurrent = 0;
                int yCurrent = 0;
                int numberOfTreesHit = 0;
                int loopMax = (int)Math.Ceiling(linesInFile / (yMovementOptions[j] + 0f));

                for (i = 0; i < loopMax ; i++)
                {
                    if (tree == pisteLines[yCurrent][xCurrent]) { numberOfTreesHit++; }
                    xCurrent += xMovementOptions[j];
                    yCurrent += yMovementOptions[j];
                    xCurrent %= xMax;
                }
                Console.WriteLine(numberOfTreesHit.ToString());
                multiplicationResult *= numberOfTreesHit;
                Console.WriteLine(multiplicationResult.ToString());

            }

            Console.WriteLine(multiplicationResult.ToString());
        }
    }
}