using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD8A
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
        public long Accumulator { get; private set; }

        bool PerformOperation()
        {
            if (operationCalculated[currentOperation])
            {
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

        public long ProcessUntilLoop()
        {
            while (PerformOperation()) { continue; } ;
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

            CodeInterpreter ci = new CodeInterpreter();
            ci.Operations = allOperations;
            Console.WriteLine(ci.ProcessUntilLoop().ToString());
        }
    }
}
