using System;
using System.IO;
using System.Linq;

namespace AoCD3A
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
            int xCurrent = 0;
            int yCurrent = 0;
            int xMovement = 3;
            int yMovement = 1;
            int xMax = 0;
            int numberOfTreesHit = 0;
            string line;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(InputFileLocation + InputFileName);

            while ((line = file.ReadLine()) != null)
            {
                pisteLines[i] = line;
                i++;
            }

            xMax = pisteLines[0].Length;

            for (i = 0; i < Math.Ceiling(linesInFile / (yMovement + 0f)); i++)
            {
                if (tree == pisteLines[yCurrent][xCurrent]) { numberOfTreesHit++; }
                xCurrent += xMovement;
                yCurrent += yMovement;
                xCurrent %= xMax;
            }

            Console.WriteLine(numberOfTreesHit.ToString());
        }
    }
}

