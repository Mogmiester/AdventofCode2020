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

            Dictionary<string, HashSet<int>> validNumbers = new Dictionary<string, HashSet<int>>();
            Dictionary<string, HashSet<int>> potentialFields = new Dictionary<string, HashSet<int>>();
            long runningErrorRate = 0;

            foreach (string rule in rules) //we're building up a hashset of integers so that we can check if a number can go into a field
            {
                string[] ruleNumberRanges = rule.Split(rulesSplit, StringSplitOptions.RemoveEmptyEntries); //0 is field name, 1&3 start, 2&4 end

                HashSet<int> numbersValidForRule = new HashSet<int>();

                Int32.TryParse(ruleNumberRanges[1], out int loopStartA);
                Int32.TryParse(ruleNumberRanges[2], out int loopEndA);
                Int32.TryParse(ruleNumberRanges[3], out int loopStartB);
                Int32.TryParse(ruleNumberRanges[4], out int loopEndB);

                for (int i = loopStartA ; i <= loopEndA; i++)
                {
                    numbersValidForRule.Add(i);
                }
                for (int i = loopStartB; i <= loopEndB; i++)
                {
                    numbersValidForRule.Add(i);
                }

                validNumbers.Add(ruleNumberRanges[0], numbersValidForRule);

            }

            foreach (KeyValuePair<string, HashSet<int>> rulesNumbers in validNumbers)   //here we have a dictionary of the potential columns that each field could be. If we find
                                                                                        //an entry that doesn't respect the rules we can remove that column from the field's potential list
            {
                HashSet<int> potentialFieldsValue = new HashSet<int>();
                for (int i = 0; i < validNumbers.Count; i++)
                {
                    potentialFieldsValue.Add(i);
                }
                potentialFields.Add(rulesNumbers.Key, potentialFieldsValue);
            }

            string[] nearbyTicketsSplit = { "\n" };
            string[] nearbyTickets = splitFile[2].Split(nearbyTicketsSplit, StringSplitOptions.RemoveEmptyEntries);
            string[] ticketNumberSplit = { "," };

            foreach (string nearbyTicket in nearbyTickets)
            {
                bool ticketValid = true;
                string[] numbersOnTicket = nearbyTicket.Split(ticketNumberSplit, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, HashSet<int>> ticketPotentialFields = new Dictionary<string, HashSet<int>>();

                for(int i = 0; i < numbersOnTicket.Length; i++)
                {
                    string numberOnTicket = numbersOnTicket[i];
                    bool numberValid = false;
                    Int32.TryParse(numberOnTicket, out int formattedNumber);

                    foreach (KeyValuePair<string, HashSet<int>> ruleRanges in validNumbers)
                    {
                        if (ruleRanges.Value.Contains(formattedNumber)) //this number can fit in this field, so we'll add this column to the potential fields dictionary
                        {
                            numberValid = true;
                            if (!ticketPotentialFields.ContainsKey(ruleRanges.Key))
                            {
                                HashSet<int> newValue = new HashSet<int>();
                                ticketPotentialFields.Add(ruleRanges.Key, newValue);
                            }
                            ticketPotentialFields[ruleRanges.Key].Add(i);
                        }
                    }
                    if (!numberValid)
                    {
                        ticketValid = false;
                        runningErrorRate += formattedNumber;
                    }
                }

                if (ticketValid) //if our ticket is valid then we use the information about potential fields
                {
                    foreach (KeyValuePair<string, HashSet<int>> potentialField in potentialFields)
                    {
                        potentialField.Value.IntersectWith(ticketPotentialFields[potentialField.Key]);
                    }
                }

            }
            Console.WriteLine(runningErrorRate.ToString());

            string[] yourTicketNumbers = splitFile[1].Split(ticketNumberSplit, StringSplitOptions.RemoveEmptyEntries);
            int[] yourTicketNumbersFormatted = new int[yourTicketNumbers.Length];

            for (int i = 0; i < yourTicketNumbers.Length; i++)
            {
                Int32.TryParse(yourTicketNumbers[i], out yourTicketNumbersFormatted[i]);
            }

            foreach(KeyValuePair<string, HashSet<int>> potentialField in potentialFields)
            {
                HashSet<int> workingHashSet = new HashSet<int>();
                workingHashSet.UnionWith(potentialField.Value);
                foreach (KeyValuePair<string, HashSet<int>> potentialField2 in potentialFields)
                {
                    if (potentialField.Key == potentialField2.Key) { continue; }
                    if (potentialField.Value.Count < potentialField2.Value.Count)
                    {
                        potentialField2.Value.RemoveWhere(match => potentialField.Value.Contains(match));
                    }
                    else if (potentialField.Value.Count > potentialField2.Value.Count)
                    {
                        workingHashSet.RemoveWhere(match => potentialField2.Value.Contains(match));
                    }
                }

                potentialField.Value.IntersectWith(workingHashSet);

            }

            long runningMultiplier = 1;

            foreach (KeyValuePair<string, HashSet<int>> potentialField in potentialFields)
            {
                if (potentialField.Key.Contains("departure"))
                {
                    foreach (int i in potentialField.Value)
                    {
                        runningMultiplier *= yourTicketNumbersFormatted[i];
                    }
                }
            }
            Console.WriteLine(runningMultiplier.ToString());
        }
    }
}
