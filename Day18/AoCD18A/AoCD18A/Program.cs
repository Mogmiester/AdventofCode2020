using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD18A
{
    class Program
    {
        static long ParseString(string parseTarget)
        {
            bool parenthesisFound = false;
            bool seekingClosingParenthesis = false;
            int parenthesisCounter = 0;
            for (int i = parseTarget.Length; i > 0; i--)
            {
                char parseChar = parseTarget[i - 1];
                if (parseChar == ')') { parenthesisFound = true; seekingClosingParenthesis = true; }
                if (seekingClosingParenthesis)
                {
                    if (parseChar == ')')
                    {
                        parenthesisCounter++;
                    }
                    else if (parseChar == '(')
                    {
                        parenthesisCounter--;
                    }
                    if (parenthesisCounter == 0) //we've found a complete set of parentheses, break our string into two parts and calculate recursively
                    {
                        seekingClosingParenthesis = false;
                    }
                }
                else
                {
                    string firstString = "";
                    if (parenthesisFound)
                    {
                        firstString = parseTarget[(i+1)..^1];
                    }
                    else
                    {
                        firstString = parseTarget[i..];
                    }
                    string secondString = parseTarget[0..(i-1)];
                    if (parseChar == '+') { return ParseString(firstString) + ParseString(secondString); }
                    if (parseChar == '*') { return ParseString(firstString) * ParseString(secondString); }
                    continue;
                }
            }
            if (parenthesisFound) //we've reached the end of the string, so the last expression was enclosed in parentheses. Remove these and reparse.
            {
                return ParseString(parseTarget[1..(parseTarget.Length - 1)]);
            }
            else //we have a number, parse it to int and return
            {
                Int32.TryParse(parseTarget, out int numberResult);
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
