using System;
using System.IO;

namespace AoCD11B
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day11A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] floorPlanRows = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            int[,] floorPlan = new int[floorPlanRows[0].Length, floorPlanRows.Length];
            (int, int)[] directions = { (1,1), (1,0), (1,-1), (0,1), (0,-1), (-1,1), (-1,0), (-1,-1)};

            for (int i = 0; i < floorPlan.GetLength(0); i++)
            {
                for (int j = 0; j < floorPlan.GetLength(1); j++)
                {
                    if (floorPlanRows[j][i] == 'L')
                    {
                        floorPlan[i, j] = 0;
                    }
                    else if (floorPlanRows[j][i] == '.')
                    {
                        floorPlan[i, j] = -1;
                    }
                    else
                    {
                        Console.WriteLine("Unexpected character in input file");
                    }
                }
            }

            bool floorPlanChanged = true;
            int runningTotal = 0;
            int loopcounter = 0;

            while (floorPlanChanged)
            {
                loopcounter++;
                floorPlanChanged = false;
                int[,] updatedFloorPlan = new int[floorPlan.GetLength(0), floorPlan.GetLength(1)];
                for (int i = 0; i < floorPlan.GetLength(0); i++)
                {
                    for (int j = 0; j < floorPlan.GetLength(1); j++)
                    {

                        if (floorPlan[i, j] == -1)
                        {
                            updatedFloorPlan[i, j] = -1;
                            continue;
                        }

                        int neighbours = 0;

                        for (int k = 0; k < directions.Length; k++)
                        {
                            bool continueLooping = true;
                            int l = 1;
                            while (continueLooping)
                            {
                                int xCoord = i + directions[k].Item1 * l;
                                int yCoord = j + directions[k].Item2 * l;

                                if (xCoord < 0 || xCoord >= floorPlan.GetLength(0) || yCoord < 0 || yCoord >= floorPlan.GetLength(1) || floorPlan[xCoord, yCoord] == 0)
                                {
                                    continueLooping = false;
                                }
                                else if (floorPlan[xCoord, yCoord] == 1)
                                {
                                    neighbours += 1;
                                    continueLooping = false;
                                }
                                else
                                {
                                    l++;
                                }
                            }   
                        }

                        if (neighbours == 0 && floorPlan[i, j] == 0)
                        {
                            floorPlanChanged = true;
                            updatedFloorPlan[i, j] = 1;
                        }
                        else if (neighbours >= 5 && floorPlan[i, j] == 1)
                        {
                            floorPlanChanged = true;
                            updatedFloorPlan[i, j] = 0;
                        }
                        else
                        {
                            updatedFloorPlan[i, j] = floorPlan[i, j];
                        }
                    }
                }

                runningTotal = 0;

                for (int i = 0; i < floorPlan.GetLength(0); i++)
                {
                    for (int j = 0; j < floorPlan.GetLength(1); j++)
                    {

                        floorPlan[i, j] = updatedFloorPlan[i, j];
                        runningTotal += Math.Max(floorPlan[i, j], 0);
                    }
                }
            }

            Console.WriteLine(runningTotal.ToString());
            for (int i = 0; i < floorPlan.GetLength(0); i++)
            {
                string outputMap = "";
                for (int j = 0; j < floorPlan.GetLength(1); j++)
                {
                    if (floorPlan[i, j] == 0)
                    {
                        outputMap += "L";
                    }
                    else if (floorPlan[i, j] == 1)
                    {
                        outputMap += "#";
                    }
                    else
                    {
                        outputMap += ".";
                    }
                }
                Console.WriteLine(outputMap);
            }
        }
    }
}
