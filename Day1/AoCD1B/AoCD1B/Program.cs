using System;

namespace AoCD1B
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
            for (i = 0; i < 200; i++)
            {
                for (int k = 0; k < 200; k++)
                {
                    for (int j = 200; j > 0; j--)
                    {
                        int testresults = InputArray[i] + InputArray[j - 1] + InputArray[k];
                        if (testresults < 2020)
                        {
                            break;
                        }
                        else if (testresults == 2020)
                        {
                            Console.WriteLine((InputArray[i] * InputArray[j - 1] * InputArray[k]).ToString());
                            return;
                        }
                    }
                }
            }
        }
    }
}
