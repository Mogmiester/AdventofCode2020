using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoCD14
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day14A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, bool[]> maskDictionary = new Dictionary<string, bool[]>();
            Dictionary<string, long> memoryValues = new Dictionary<string, long>();

            for (int i = 0; i < splitFile.Length; i++)
            {
                if (splitFile[i][1] == 'a') //mask
                {
                    maskDictionary.Clear();
                    maskDictionary.Add("", new bool[36]);
                    string maskString = splitFile[i].Split(" = ")[1];
                    for (int j = 0; j < maskString.Length; j++)
                    {
                        if (maskString[j] == 'X') //floating, need to double up existing strings
                        {
                            Dictionary<string, bool[]> newMaskDictionary = new Dictionary<string, bool[]>();
                            foreach (KeyValuePair<string, bool[]> mask in maskDictionary)
                            {
                                bool[] newOverrideFlag0 = mask.Value;
                                newOverrideFlag0[j] = true;
                                bool[] newOverrideFlag1 = mask.Value;
                                newOverrideFlag1[j] = true;
                                newMaskDictionary.Add(mask.Key + "0", newOverrideFlag0);
                                newMaskDictionary.Add(mask.Key + "1", newOverrideFlag1);
                            }
                            maskDictionary.Clear();
                            maskDictionary = newMaskDictionary;
                        }
                        else if (maskString[j] == '1' || maskString[j] == '0')
                        {
                            Dictionary<string, bool[]> newMaskDictionary = new Dictionary<string, bool[]>();
                            foreach (KeyValuePair<string, bool[]> mask in maskDictionary)
                            {
                                newMaskDictionary.Add(mask.Key + maskString[j], mask.Value);
                            }
                            maskDictionary.Clear();
                            maskDictionary = newMaskDictionary;
                        }
                        else
                        {
                            Console.WriteLine("Panic: Unknown mask digit on line" + (i + 1).ToString());
                        }
                    }
                }
                else if (splitFile[i][1] == 'e') // memory assignment
                {
                    string[] split = { "] = ", "[", };
                    string[] instructionString = splitFile[i].Split(split, StringSplitOptions.RemoveEmptyEntries);
                    Int64.TryParse(instructionString[2], out long assignmentValue);
                    Int64.TryParse(instructionString[1], out long initialMemoryLocation);
                    string initialBinaryString = Convert.ToString(initialMemoryLocation, 2);
                    string leftPadding = "";
                    for (int j = 0; j < maskDictionary.First().Key.Length - initialBinaryString.Length; j++)
                    {
                        leftPadding += "0";
                    }
                    initialBinaryString = leftPadding + initialBinaryString;
                    foreach (KeyValuePair<string, bool[]> mask in maskDictionary)
                    {
                        string memoryLocation = "";
                        for (int j = 0; j < mask.Key.Length; j++)
                        {
                            if (mask.Value[j])
                            {
                                memoryLocation += mask.Key[j];
                            }
                            else if (mask.Key[j] == '1' || initialBinaryString[j] == '1')
                            {
                                memoryLocation += '1';
                            }
                            else
                            {
                                memoryLocation += '0';
                            }
                        }
                        memoryValues[memoryLocation] = assignmentValue;
                    }
                }
                else
                {
                    Console.WriteLine("Panic: Unknown instruction on line" + (i + 1).ToString());
                }
            }
            long runningTotal = 0;
            foreach (KeyValuePair<string, long> keyValuePair in memoryValues)
            {
                runningTotal += keyValuePair.Value;
            }
            Console.WriteLine(runningTotal.ToString());
        }
    }
}
