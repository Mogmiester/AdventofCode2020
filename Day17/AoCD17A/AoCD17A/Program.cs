using System;
using System.IO;
using System.Collections.Generic;

namespace AoCD17A
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\AdventofCode2020\Inputs\";
            string InputFileName = "Day17A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");

            string[] initialSplit = { "\n" };
            string[] splitFile = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<(int, int, int), int> boardState = new Dictionary<(int, int, int), int>();
            

            for (int i = 0; i < splitFile.Length; i++)
            {
                for (int j = 0; j < splitFile[i].Length; j++)
                {
                    char boardEntry = splitFile[i][j];
                    if (boardEntry == '#') //this board position has an active cell
                    {
                        boardState.Add((i, j, 0), 1); //we're actually flipping the x/y coords here, but no harm done
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                boardState = EvolveBoard(boardState, 2, 3);
            }
            Console.WriteLine(boardState.Count);
        }

        static Dictionary<(int, int, int), int> EvolveBoard(Dictionary<(int x, int y, int z), int> boardState, int neighboursRequiredToStayOn, int neighboursRequiredToTurnOn)
        {
            Dictionary<(int x, int y, int z), int> numberOfNeighbours = new Dictionary<(int, int, int), int>();
            Dictionary<(int x, int y, int z), int> updatedBoardState = new Dictionary<(int, int, int), int>();

            foreach (KeyValuePair<(int x, int y, int z), int> boardEntry in boardState)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            if (i == 0 && j == 0 && k == 0) { continue; }
                            (int, int, int) coordinates = (boardEntry.Key.x + i, boardEntry.Key.y + j, boardEntry.Key.z + k);
                            if (!numberOfNeighbours.ContainsKey(coordinates)) { numberOfNeighbours.Add(coordinates, 0); }
                            numberOfNeighbours[coordinates] += 1;
                        }
                    }
                }
            }
            foreach (KeyValuePair<(int x, int y, int z), int> potentialBoardEntry in numberOfNeighbours)
            {
                if (potentialBoardEntry.Value == neighboursRequiredToTurnOn
                        || (potentialBoardEntry.Value == neighboursRequiredToStayOn && boardState.ContainsKey(potentialBoardEntry.Key) && boardState[potentialBoardEntry.Key] == 1))
                {
                    updatedBoardState[potentialBoardEntry.Key] = 1;
                }
            }

            return updatedBoardState;
        }

    }
}
