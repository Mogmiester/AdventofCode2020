using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace AoCD7A
{
    public static class Graph //this is a disgrace
    {
        public static Dictionary<string, Node> AllNodes = new Dictionary<string, Node>();

        public static void AddParent(string child, string parent)
        {

            if (!AllNodes.TryGetValue(child, out Node childNode))
            {
                childNode = new Node();
                AllNodes.Add(child, childNode);
            }

            if (!AllNodes.TryGetValue(parent, out Node parentNode))
            {
                parentNode = new Node();
                AllNodes.Add(parent, parentNode);
            }

            if (!childNode.parentNodes.ContainsKey(parent))
            {
                childNode.parentNodes.Add(parent, parentNode);
            }
        }
    }
    public class Node
    {
        public readonly string Name;
        public Dictionary<string, Node> parentNodes = new Dictionary<string, Node>();
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
                string cleanLine = line.Replace("bags", "").Replace("bag", "").Replace(" ", "").Replace(".", "");
                for (int i = 0; i < 10; i++)
                {
                    cleanLine = cleanLine.Replace(i.ToString(), "");
                }
                string[] rulesSplit = { "contain" };
                string[] rule = cleanLine.Split(rulesSplit, StringSplitOptions.None);
                string parentBag = rule[0];
                string[] childBags = rule[1].Split(",");
                foreach(string childBag in childBags)
                {
                    Graph.AddParent(childBag, parentBag);
                }
            }
            Console.WriteLine((GetHashSetOfAllParents("shinygold").Count - 1).ToString());
        }

        public static HashSet<string> GetHashSetOfAllParents(string child)
        {
            HashSet<string> workingHashSet = new HashSet<string>();
            workingHashSet.Add(child);
            Graph.AllNodes.TryGetValue(child, out Node childNode);
            foreach ((string name, Node parentNode) in childNode.parentNodes)
            {
                workingHashSet.UnionWith(GetHashSetOfAllParents(name));
            }
            return workingHashSet;
        }
    }
}