using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD8B
{
    class CodeInterpreter
    {
        private int currentOperation = 0;
        private string[] _operations;
        public string[] Operations
        {
            get
            {
                return _operations;
            }

            set
            {
                _operations = value;
                operationCalculated = new bool[_operations.Length];
            }
        }
        private bool[] operationCalculated;
        public bool SuccessfulBoot { get; private set; }
        public bool InfiniteLoop { get; private set; }
        public bool OutOfBoundsOperationAttempt { get; private set; }
        public long Accumulator { get; private set; }

        bool PerformOperation()
        {
            if (currentOperation == _operations.Length)
            {
                SuccessfulBoot = true;
                return false;
            }
            else if (currentOperation > _operations.Length || currentOperation < 0)
            {
                OutOfBoundsOperationAttempt = true;
                return false;
            }
            if (operationCalculated[currentOperation])
            {
                InfiniteLoop = true;
                return false;
            }
            else
            {
                operationCalculated[currentOperation] = true;
                string codeLine = Operations[currentOperation];
                string[] codeParts = codeLine.Split(" ");
                Int32.TryParse(codeParts[1], out int operationArgument);
                switch (codeParts[0])
                {
                    case "jmp":
                        currentOperation += operationArgument;
                        break;
                    case "acc":
                        Accumulator += operationArgument;
                        currentOperation++;
                        break;
                    case "nop":
                    default:
                        currentOperation++;
                        break;
                }
                return true;
            }
        }

        public long ProcessUntilError()
        {
            while (PerformOperation()) { continue; };
            return Accumulator;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.MainProgram();
        }

        void MainProgram()
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day8A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] allOperations = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < allOperations.Length; i++)
            {
                if (allOperations[i][0..3] == "nop" || allOperations[i][0..3] == "jmp")
                {
                    string[] alteredAllOperations = new string[allOperations.Length];
                    for (int j = 0; j < allOperations.Length; j++)
                    {
                        alteredAllOperations[j] = allOperations[j];
                    }

                    if (allOperations[i][0..3] == "nop")
                    {
                        alteredAllOperations[i] = "jmp" + allOperations[i][3..];
                    }
                    else
                    {
                        alteredAllOperations[i] = "nop" + allOperations[i][3..];
                    }

                    CodeInterpreter ci = new CodeInterpreter();
                    ci.Operations = alteredAllOperations;
                    long accResult = ci.ProcessUntilError();
                    if (ci.SuccessfulBoot) { Console.WriteLine(accResult.ToString()); }
                }
            }
        }
    }
}

