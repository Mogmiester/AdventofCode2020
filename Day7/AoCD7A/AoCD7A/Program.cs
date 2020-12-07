using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace AoCD7A
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "STCDay07.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = new string[1];
            initialSplit[0] = "\n";
            string[] allRules = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, HashSet<string>> directedGraph = new Dictionary<string, HashSet<string>>();


            foreach (string line in allRules)
            {
                string cleanLine = line.Replace("bags", "").Replace("bag", "").Replace(" ", "").Replace(".", "");
                for (int i = 0; i < 10; i++)
                {
                    cleanLine = cleanLine.Replace(i.ToString(), "");
                }
                string[] rulesSplit = new string[1];
                rulesSplit[0] = "contain";
                string[] rule = cleanLine.Split(rulesSplit, StringSplitOptions.None);
                string parentBag = rule[0];
                string[] childBags = rule[1].Split(",");
                foreach(string childBag in childBags)
                {
                    if (!directedGraph.TryGetValue(childBag, out HashSet<string> currentContaintingBags))
                    {
                        currentContaintingBags = new HashSet<string>();
                        currentContaintingBags.Add(parentBag);
                        directedGraph.Add(childBag, currentContaintingBags);
                    }
                    else
                    {
                        currentContaintingBags.Add(parentBag);
                    }
                }
            }

            HashSet<string> answerSet = new HashSet<string>();
            HashSet<string> processedBags = new HashSet<string>();
            answerSet.Add("shinygold");
            bool continueProcessing = true;

            while (continueProcessing)
            {
                HashSet<string> processingAnswerSet = new HashSet<string>(answerSet);
                foreach (string childBag in processingAnswerSet)
                {
                    if (directedGraph.TryGetValue(childBag, out HashSet<string> containingBags))
                    {
                        foreach (string containgingBag in containingBags)
                        {
                            answerSet.Add(containgingBag);
                        }
                    }
                    processedBags.Add(childBag);
                }

                answerSet.ExceptWith(processingAnswerSet);
                if (answerSet.Count == 0)
                {
                    continueProcessing = false;
                }
            }

            Console.WriteLine(processedBags.Count - 1);
        }
    }
}