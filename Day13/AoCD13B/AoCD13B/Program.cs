using System;
using System.IO;

namespace AoCD13A
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day13A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            string[] busIDSplit = { "," };
            string[] busIDs = splitFile[1].Split(busIDSplit, StringSplitOptions.RemoveEmptyEntries);
            if (!Int64.TryParse(busIDs[0], out long currentMultiple)) { Console.WriteLine("Panic: Couldn't parse bus ID for bus " + busIDs[0]); }
            long currentOffset = 0;
            for (int i = 1; i < busIDs.Length; i++)
            {
                if (busIDs[i] == "x") { continue; }
                if (!Int32.TryParse(busIDs[i], out int busID)) { Console.WriteLine("Panic: Couldn't parse bus ID for bus " + busIDs[i]); }
                bool continueLooping = true;
                long j = 0;
                while (continueLooping)
                {
                    if ((j * currentMultiple + i + currentOffset) % busID == 0)
                    {
                        currentOffset = j * currentMultiple + currentOffset;
                        currentMultiple *= busID;
                        continueLooping = false;
                    }
                    else
                    {
                        j++;
                    }
                }
                
            }

            Console.WriteLine(currentOffset.ToString());

        }
    }
}
