using System;

namespace AoCD1A
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day1A.txt";
            int[] InputArray = new int[200];
            int i = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(InputFileLocation + InputFileName);
            while ((line = file.ReadLine()) != null)
            {
                int.TryParse(line, out InputArray[i]);
                i++;
            }

            Console.WriteLine(@"Input file read sucessfully");
            Array.Sort(InputArray);
            for (i = 0; i < 200; i++) {
                for (int j = 200; j > 0 ; j--)
                {
                    if ((InputArray[i] + InputArray[j - 1]) < 2020)
                    {
                        break;
                    }
                    else if ((InputArray[i] + InputArray[j - 1]) == 2020)
                    {
                        Console.WriteLine((InputArray[i] * InputArray[j - 1]).ToString());
                        return;
                    }
                }
            }
        }
    }
}
