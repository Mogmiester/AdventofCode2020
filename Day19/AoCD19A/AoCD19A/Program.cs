using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD19A
{
    class RulesInterpreter
    {
        private Dictionary<int, Dictionary<int, Dictionary<int, (int RuleValue, bool FinalRule)>>> RuleSet = new Dictionary<int, Dictionary<int, Dictionary<int, (int, bool)>>>();
        //ruleID, rule option, rule position, child rule ID or string to match, if it's a final rule
        public RulesInterpreter(string[] rules)
        {
            foreach (string rule in rules)
            {
                string[] ruleIDsplit = { ": " };
                string[] ruleStringComponents = rule.Split(ruleIDsplit, StringSplitOptions.RemoveEmptyEntries);
                string[] ruleSets;

                if (!Int32.TryParse(ruleStringComponents[0], out int ruleID)) { Console.WriteLine(@"Panic! Couldn't parse rule ID " + ruleStringComponents[0]); }
                if (ruleStringComponents[1].Contains("\"")) //final rule - either a (=0) or b (=1)
                {
                    string[] endrule = ruleStringComponents[1].Split('"', StringSplitOptions.RemoveEmptyEntries);
                    if (endrule[0] == "a")
                    {
                        RuleSet.Add(ruleID, new Dictionary<int, Dictionary<int, (int, bool)>>() { { 0, new Dictionary<int, (int, bool)> { { 0, (0, true) } } } });
                    }
                    else
                    {
                        RuleSet.Add(ruleID, new Dictionary<int, Dictionary<int, (int, bool)>>() { { 0, new Dictionary<int, (int, bool)> { { 0, (1, true) } } } });
                    }
                }
                else
                {

                    ruleSets = ruleStringComponents[1].Split('|');

                    for (int i = 0; i < ruleSets.Length; i++)
                    {
                        string[] childRuleIDs = ruleSets[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < childRuleIDs.Length; j++)
                        {
                            if (!Int32.TryParse(childRuleIDs[j], out int childRuleID))
                            { Console.WriteLine(@"Panic! Couldn't parse child rule ID " + childRuleIDs[j] + "for rule " + ruleID.ToString() + "."); }

                            Dictionary<int, Dictionary<int, (int, bool)>> outerDict;
                            Dictionary<int, (int, bool)> innerDict;
                            if (RuleSet.ContainsKey(ruleID))
                            {
                                outerDict = RuleSet[ruleID];
                            }
                            else
                            {
                                outerDict = new Dictionary<int, Dictionary<int, (int, bool)>>();
                                RuleSet.Add(ruleID, outerDict);
                            }

                            if (outerDict.ContainsKey(i))
                            {
                                innerDict = outerDict[i];
                            }
                            else
                            {
                                innerDict = new Dictionary<int, (int, bool)>();
                                outerDict.Add(i, innerDict);
                            }
                            innerDict[j] = (childRuleID, false);
                        }
                    }
                }
            }
        }
        
        public bool CheckMessage(string message) //to be a valid message a string must match rule 0
        {
            bool matchesRules = CheckSubString(message, 0, out string remainingMessage);
            return matchesRules && remainingMessage == "";
        }

        private bool CheckSubString(string message, int ruleID, out string remainingMessage)
        {
            if (message == "")
            {
                remainingMessage = "";
                return false;
            }
            if (RuleSet[ruleID][0][0].FinalRule)
            {
                remainingMessage = message[1..];
                return (message[0] == 'a' && RuleSet[ruleID][0][0].RuleValue == 0) || (message[0] == 'b' && RuleSet[ruleID][0][0].RuleValue == 1);
            }
            else
            {
                foreach (var ruleOptions in RuleSet[ruleID]) //these are options - either can be true
                {
                    bool ruleOptionValid = true;
                    string messageNotConsumed = message;

                    for (int i = 0; i < ruleOptions.Value.Count; i++)
                    {
                        if(!CheckSubString(messageNotConsumed, ruleOptions.Value[i].RuleValue, out messageNotConsumed))
                        {
                            ruleOptionValid = false;
                            break;
                        }
                    }
                    if (ruleOptionValid)
                    {
                        remainingMessage = messageNotConsumed;
                        return true;
                    }
                }
            }
            remainingMessage = message;
            return false;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\AdventofCode2020\Inputs\";
            string InputFileName = "Day19A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");

            string[] initialSplit = { "\n\n" };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries); //0 is rules, 1 is candidate strings
            string[] rules = splitFile[0].Split('\n', StringSplitOptions.RemoveEmptyEntries);

            RulesInterpreter rulesInterpreter = new RulesInterpreter(rules);

            string[] messages = splitFile[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int runningTotal = 0;
            foreach (string message in messages)
            {
                if (rulesInterpreter.CheckMessage(message)) { runningTotal++; }
            }
            Console.WriteLine(runningTotal.ToString());
        }
    }
}
