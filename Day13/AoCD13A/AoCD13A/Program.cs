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
            if(!Int32.TryParse(splitFile[0], out int initialTime)){ Console.WriteLine("Panic: Couldn't parse current time."); }
            string[] busIDSplit = { "," };
            string[] busIDs = splitFile[1].Split(busIDSplit, StringSplitOptions.RemoveEmptyEntries);
            int shortestWait = int.MaxValue;
            int resultA = 0;
            for (int i = 0; i < busIDs.Length; i++)
            {
                if(busIDs[i] == "x") { continue; }
                if(!Int32.TryParse(busIDs[i], out int busID)) { Console.WriteLine("Panic: Couldn't parse bus ID for bus " + busIDs[i]); }
                int waitingTime = busID - initialTime % busID;
                if (waitingTime < shortestWait)
                {
                    shortestWait = waitingTime;
                    resultA = shortestWait * busID;
                }
            }

            Console.WriteLine(resultA.ToString());

        }
    }
}
