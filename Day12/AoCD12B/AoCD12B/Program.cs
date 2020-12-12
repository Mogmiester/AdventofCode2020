using System;
using System.IO;

namespace AoCD12B
{
    class Ship
    {
        private (float x, float y) location;
        private (float x, float y) waypointOffset = (10f, 1f);
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

            float waypointDistance = MathF.Pow(MathF.Pow(waypointOffset.x, 2f) + MathF.Pow(waypointOffset.y, 2f), 0.5f);
            float currentWaypointX = waypointOffset.x;
            float currentWaypointY = waypointOffset.y;

            switch (direction[0])
            {
                case 'N':
                    waypointOffset.y += magnitude;
                    return true;
                case 'S':
                    waypointOffset.y -= magnitude;
                    return true;
                case 'E':
                    waypointOffset.x += magnitude;
                    return true;
                case 'W':
                    waypointOffset.x -= magnitude;
                    return true;
                case 'L':
                    magnitude %= 360;
                    switch (magnitude)
                    {
                        case 90:
                            waypointOffset.x = -currentWaypointY;
                            waypointOffset.y = currentWaypointX;
                            return true;
                        case 180:
                            waypointOffset.x = -currentWaypointX;
                            waypointOffset.y = -currentWaypointY;
                            return true;
                        case 270:
                            waypointOffset.x = currentWaypointY;
                            waypointOffset.y = -currentWaypointX;
                            return true;
                        case 0:
                            return true;
                        default:
                            Console.WriteLine("Panic: Unknown turn");
                            return false;
                    }
                case 'R':
                    magnitude %= 360;
                    switch (magnitude)
                    {

                        case 90:
                            waypointOffset.x = currentWaypointY;
                            waypointOffset.y = -currentWaypointX;
                            return true;
                        case 180:
                            waypointOffset.x = -currentWaypointX;
                            waypointOffset.y = -currentWaypointY;
                            return true;
                        case 270:
                            waypointOffset.x = -currentWaypointY;
                            waypointOffset.y = currentWaypointX;
                            return true;
                        case 0:
                            return true;
                        default:
                            Console.WriteLine("Panic: Unknown turn");
                            return false;
                    }
                case 'F':
                    location.x += magnitude * waypointOffset.x;
                    location.y += magnitude * waypointOffset.y;
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
