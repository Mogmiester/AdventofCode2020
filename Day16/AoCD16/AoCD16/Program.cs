using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD16
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\AdventofCode2020\Inputs\";
            string InputFileName = "Day16A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");

            string[] initialSplit = { "nearby tickets:\n", "your ticket:\n" };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries); //0 is rules, 1 is your ticket, 2 is nearby tickets

            string[] rules = splitFile[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
            string[] rulesSplit = { ": ", " or ", "-" };

            HashSet<int> validNumbers = new HashSet<int>();
            long runningErrorRate = 0;

            foreach (string rule in rules)
            {
                string[] ruleNumberRanges = rule.Split(rulesSplit, StringSplitOptions.RemoveEmptyEntries); //0 is field name, 1&3 start, 2&4 end

                Int32.TryParse(ruleNumberRanges[1], out int loopStartA);
                Int32.TryParse(ruleNumberRanges[2], out int loopEndA);
                Int32.TryParse(ruleNumberRanges[3], out int loopStartB);
                Int32.TryParse(ruleNumberRanges[4], out int loopEndB);

                for (int i = loopStartA ; i <= loopEndA; i++)
                {
                    validNumbers.Add(i);
                }
                for (int i = loopStartB; i <= loopEndB; i++)
                {
                    validNumbers.Add(i);
                }
            }

            string[] nearbyTicketsSplit = {"\n", "," };
            string[] numbersOnNearbyTickets = splitFile[2].Split(nearbyTicketsSplit, StringSplitOptions.RemoveEmptyEntries);

            foreach (string numberOnNearbyTickets in numbersOnNearbyTickets)
            {
                Int32.TryParse(numberOnNearbyTickets, out int formattedNumberOnNearbyTicket);
                if (!validNumbers.Contains(formattedNumberOnNearbyTicket))
                {
                    runningErrorRate += formattedNumberOnNearbyTicket;
                }
            }

            Console.WriteLine(runningErrorRate.ToString());


        }
    }
}
