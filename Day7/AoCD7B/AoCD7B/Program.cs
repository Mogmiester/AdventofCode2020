using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace AoCD7B
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "STCDay07.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] allRules = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, HashSet<(int, string)>> directedGraph = new Dictionary<string, HashSet<(int, string)>>();
            HashSet<string> numbers = new HashSet<string>();

            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i.ToString());
            }

            foreach (string line in allRules)
            {
                string cleanLine = line.Replace("bags", "").Replace("bag", "").Replace(" ", "").Replace(".", "");
                string[] rulesSplit = { "contain" };
                string[] rule = cleanLine.Split(rulesSplit, StringSplitOptions.None);
                string parentBag = rule[0];
                string[] childBags = rule[1].Split(",");
                HashSet<(int, string)> bagContains = new HashSet<(int, string)>();
                //process numbers, remove from child bags
                foreach (string childBag in childBags)
                {
                    string number = "";
                    string cleanChildBag = "";
                    for (int i = 0; i < childBag.Length; i++)
                    {
                        if (numbers.Contains(childBag[i].ToString()))
                        {
                            number += childBag[i].ToString();
                        }
                        else
                        {
                            cleanChildBag = childBag[i..];
                            break;
                        }
                    }

                    int convertedNumber = 0;
                    if (!Int32.TryParse(number, out convertedNumber))
                    {
                        convertedNumber = 1;
                    }

                    bagContains.Add((convertedNumber, cleanChildBag));

                }

                directedGraph.Add(parentBag, bagContains);

            }

            Console.WriteLine((NumberOfContainedBags(directedGraph, "shinygold") - 1).ToString());

        }

        public static long NumberOfContainedBags(Dictionary<string, HashSet<(int, string)>> directedGraph, string parentBag)
        {
            directedGraph.TryGetValue(parentBag, out HashSet<(int, string)> childBags);
            long workingResult = 1;
            foreach ((int, string) childBag in childBags)
            {
                if (childBag.Item2 == "noother")
                {
                    return workingResult;
                }
                else
                {
                    workingResult += childBag.Item1 * NumberOfContainedBags(directedGraph, childBag.Item2);
                }
            }

            return workingResult;
        }
    }
}