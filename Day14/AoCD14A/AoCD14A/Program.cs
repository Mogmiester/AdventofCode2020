using System;
using System.IO;

namespace AoCD14A
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
            int[] maskValue = new int[36];
            bool[] maskSet = new bool[36];
            long[] memoryValues = new long[65536];

            for (int i = 0; i < splitFile.Length; i++)
            {
                if (splitFile[i][1] == 'a') //mask
                {
                    string maskString = splitFile[i].Split(" = ")[1];
                    for (int j = 0; j < maskValue.Length; j++)
                    {
                        if (maskString[j] == 'X')
                        {
                            maskValue[j] = 0;
                            maskSet[j] = false;
                        }
                        else if (maskString[j] == '0')
                        {
                            maskValue[j] = 0;
                            maskSet[j] = true;
                        }
                        else if (maskString[j] == '1')
                        {
                            maskValue[j] = 1;
                            maskSet[j] = true;
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
                    Int32.TryParse(instructionString[2], out int intialAssignmentValue);
                    string initialBinaryString = Convert.ToString(intialAssignmentValue, 2);
                    string leftPadding = "";
                    for (int j = 0; j < maskValue.Length - initialBinaryString.Length; j++)
                    {
                        leftPadding += "0";
                    }
                    initialBinaryString = leftPadding + initialBinaryString;
                    string binaryString = "";
                    for (int j = 0; j < maskValue.Length; j++)
                    {
                        if (maskSet[j])
                        {
                            binaryString += maskValue[j];
                        }
                        else
                        {
                            binaryString += initialBinaryString[j];
                        }
                    }
                    long assignmentValue = Convert.ToInt64(binaryString, 2);
                    Int64.TryParse(instructionString[1], out long memoryLocation);
                    memoryValues[memoryLocation] = assignmentValue;
                }
                else
                {
                    Console.WriteLine("Panic: Unknown instruction on line" + (i + 1).ToString());
                }
            }
            long runningTotal = 0;
            for (int i = 0; i < memoryValues.Length; i++)
            {
                runningTotal += memoryValues[i];
            }
            Console.WriteLine(runningTotal.ToString());
        }
    }
}
