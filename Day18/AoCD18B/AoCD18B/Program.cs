using System;
using System.IO;

namespace AoCD18B
{
    class Program
    {
        static long ParseString(string parseTarget)
        {
            bool seekingClosingParenthesis = false;
            bool additionFound = false;
            int additionLocation = 0;
            int parenthesisCounter = 0;
            int parenthesisStartLocation = 0;

            for (int i = 0; i < parseTarget.Length; i++)
            {
                char parseChar = parseTarget[i];
                if (parseChar == '(' && !seekingClosingParenthesis)
                {
                    seekingClosingParenthesis = true; 
                    parenthesisStartLocation = i; 
                }
                if (seekingClosingParenthesis)
                {
                    if (parseChar == '(')
                    {
                        parenthesisCounter++;
                    }
                    else if (parseChar == ')')
                    {
                        parenthesisCounter--;
                    }
                    if (parenthesisCounter == 0) //we've found a complete set of parentheses, calculate this first, and then update our string
                    {
                        seekingClosingParenthesis = false;

                        string newParseTarget = parseTarget[0..parenthesisStartLocation] + ParseString(parseTarget[(parenthesisStartLocation + 1)..i]).ToString() + parseTarget[(i + 1)..];

                        return ParseString(newParseTarget);
                    }
                }
                else
                {
                    if (parseChar == '*') //if we find a multiplication, split now because we want this to be calculated last.
                    {
                        string firstString = parseTarget[0..i];
                        string secondString = parseTarget[(i + 1)..];
                        return ParseString(firstString) * ParseString(secondString);
                    }
                    if (parseChar == '+' && !additionFound) //make a note of finding addition - we'll come back to it if we don't find any multiplications by the end of the string.
                    {
                        additionFound = true;
                        additionLocation = i;
                    }
                }
            }
            if (additionFound) //we found addition but not multiplication - split string on addition and calc recursively
            {
                string firstString = parseTarget[0..additionLocation];

                string secondString = parseTarget[(additionLocation + 1)..];
                return ParseString(firstString) + ParseString(secondString);
            }
            else //we have a number, parse it to int and return
            {
                if (!Int64.TryParse(parseTarget, out long numberResult)) { Console.WriteLine(@"Panic! Couldn't parse " + parseTarget); }; //lol overflow
                return numberResult;
            }
        }
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\AdventofCode2020\Inputs\";
            string InputFileName = "Day18A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");

            string[] initialSplit = { "\n" };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries); //each line is a mathematical expression we must compute

            long runningTotal = 0;

            foreach (string mathematicalExpression in splitFile)
            {
                runningTotal += ParseString(mathematicalExpression.Replace(" ", ""));
            }
            Console.WriteLine(runningTotal.ToString());
        }
    }
}
