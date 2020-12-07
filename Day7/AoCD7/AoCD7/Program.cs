﻿using System;
using System.IO;
using System.Collections.Generic;


namespace AoCD7
{
    public static class Graph //this is now slightly less of a disgrace
    {
        public static Dictionary<string, HashSet<string>> AllNodesParents = new Dictionary<string, HashSet<string>>();
        public static Dictionary<string, Dictionary<string, int>> AllNodesChildren = new Dictionary<string, Dictionary<string, int>>();

        public static void AddParent(string child, string parent)
        {

            if (!AllNodesParents.TryGetValue(child, out HashSet<string> childNode))
            {
                childNode = new HashSet<string>();
                AllNodesParents.Add(child, childNode);
            }

            if (!AllNodesParents.TryGetValue(parent, out HashSet<string> parentNode))
            {
                parentNode = new HashSet<string>();
                AllNodesParents.Add(parent, parentNode);
            }

            if (!childNode.Contains(parent))
            {
                childNode.Add(parent);
            }
        }
        public static void AddChild(string child, int weight, string parent)
        {

            if (!AllNodesChildren.TryGetValue(child, out Dictionary<string, int> childNode))
            {
                childNode = new Dictionary<string, int>();
                AllNodesChildren.Add(child, childNode);
            }

            if (!AllNodesChildren.TryGetValue(parent, out Dictionary<string, int> parentNode))
            {
                parentNode = new Dictionary<string, int>();
                AllNodesChildren.Add(parent, parentNode);
            }

            if (!parentNode.TryGetValue(child, out _))
            {
                parentNode.Add(child, weight);
            }
        }

        public static HashSet<string> GetHashSetOfAllParents(string child)
        {
            HashSet<string> workingHashSet = new HashSet<string>();
            workingHashSet.Add(child);
            Graph.AllNodesParents.TryGetValue(child, out HashSet<string> childNode);
            foreach (string name in childNode)
            {
                workingHashSet.UnionWith(GetHashSetOfAllParents(name));
            }
            return workingHashSet;
        }

        public static long NumberOfContainedBags(string parentBag)
        {
            Graph.AllNodesChildren.TryGetValue(parentBag, out Dictionary<string, int> childBags);
            long workingResult = 1;
            foreach (KeyValuePair<string, int> childBag in childBags)
            {
                if (!(childBag.Key == "noother"))
                {
                    workingResult += childBag.Value * NumberOfContainedBags(childBag.Key);
                }
            }
            return workingResult;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day7A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] allRules = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in allRules)
            {
                string cleanLine = line.Replace("bags", "").Replace("bag", "").Replace(".", "");
                string[] rulesSplit = { "contain" };
                string[] rule = cleanLine.Split(rulesSplit, StringSplitOptions.None);
                string parentBag = rule[0].Replace(" ", "");
                string[] childBagsAndWeights = rule[1].Split(",");
                foreach (string childBagAndWeight in childBagsAndWeights)
                {
                    string[] splitChildString = childBagAndWeight.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Int32.TryParse(splitChildString[0], out int weight);
                    string bagColour = "";
                    for (int i = 1; i < splitChildString.Length; i++)
                    {
                        bagColour += splitChildString[i];
                    }
                    Graph.AddParent(bagColour, parentBag);
                    Graph.AddChild(bagColour, weight, parentBag);
                }
            }
            Console.WriteLine(@"Part A" + (Graph.GetHashSetOfAllParents("shinygold").Count - 1).ToString());
            Console.WriteLine(@"Part B" + (Graph.NumberOfContainedBags("shinygold") - 1).ToString());
        }
    }
}