using System;
using System.IO;

namespace AoCD12A
{
    class Ship
    {
        private (float x, float y) location;
        private float heading;
        public float ManhattanDistanceFromOrigin()
        {
            return MathF.Abs(location.x) + MathF.Abs(location.y);
        }

        public bool DirectionInstruction(string direction)
        {
            if (!Int32.TryParse(direction[1..], out int magnitude))
            {
                Console.WriteLine("Panic: Couldn't parse direction magnitude");
                return false;
            }
            switch (direction[0])
            {
                case 'N':
                    location.x += magnitude;
                    return true;
                case 'S':
                    location.x -= magnitude;
                    return true;
                case 'E':
                    location.y += magnitude;
                    return true;
                case 'W':
                    location.y -= magnitude;
                    return true;
                case 'L':
                    heading -= magnitude;
                    heading %= 360f;
                    return true;
                case 'R':
                    heading += magnitude;
                    heading %= 360f;
                    return true;
                case 'F':
                    location.x += -MathF.Sin(heading * MathF.PI / 180) * magnitude;
                    location.y += MathF.Cos(heading * MathF.PI / 180) * magnitude;
                    return true;
                default:
                    Console.WriteLine("Panic: Unknown direction order");
                    return false;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileLocation = @"C:\Users\matta\Documents\Advent of Code\Inputs\";
            string InputFileName = "Day12A.txt";
            string fileAsString = File.ReadAllText(Path.Combine(InputFileLocation, InputFileName)).Replace("\r", "");
            string[] initialSplit = { "\n" };
            string[] directions = fileAsString.Split(initialSplit, StringSplitOptions.RemoveEmptyEntries);
            Ship ship = new Ship();
            for (int i = 0; i < directions.Length; i++)
            {
                if (!ship.DirectionInstruction(directions[i])) { break; }
            }
            Console.WriteLine(ship.ManhattanDistanceFromOrigin().ToString());
        }
    }
}
