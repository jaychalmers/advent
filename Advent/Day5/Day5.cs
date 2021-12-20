using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Year2021
{
    internal class Day5 : Solution
    {
        private static Regex LineParse => new Regex(@"([0-9]*),([0-9]*) -> ([0-9]*),([0-9]*)");
        private Dictionary<int, Dictionary<int, int>> Points;

        internal Day5()
        {
            Points = new Dictionary<int, Dictionary<int, int>>();
        }

        internal void Part1(bool includeDiagonal = false)
        {
            foreach(var line in File.ReadAllLines(_path))
            {
                var points = GetPointsAlongPath(line, includeDiagonal);
                foreach(var point in points)
                {
                    var x = point.Item1;
                    var y = point.Item2;

                    if (!Points.ContainsKey(x))
                    {
                        Points.Add(x, new Dictionary<int, int>());
                    }
                    if (!Points[x].ContainsKey(y))
                    {
                        Points[x].Add(y, 0);
                    }
                    Points[x][y]++;
                }
            }
            PrintPoints();
            Display(FlattenPoints(Points).Where(point => point.Item3 > 1).Count());
        }

        internal void Part2()
        {
            Part1(true);
        }

        private void PrintPoints()
        {
            //Only works for Part 1 - hardcoded size
            var sb = new StringBuilder();
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (Points.ContainsKey(i) && Points[i].ContainsKey(j))
                    {
                        sb.Append(Points[i][j]);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }

        private List<(int,int,int)> FlattenPoints(Dictionary<int, Dictionary<int, int>> points)
        {
            var flattenedPoints = new List<(int, int, int)>();
            foreach(var x in Points.Keys)
            {
                foreach(var y in Points[x].Keys)
                {
                    var point = (x, y, Points[x][y]);
                    flattenedPoints.Add(point);
                }
            }
            return flattenedPoints;
        }

        public IEnumerable<(int,int)> GetPointsAlongPath(string pathDescriptor, bool includeDiagonal = false)
        {
            var match = LineParse.Match(pathDescriptor);
            var startX = int.Parse(match.Groups[1].Value);
            var startY = int.Parse(match.Groups[2].Value);
            var endX = int.Parse(match.Groups[3].Value);
            var endY = int.Parse(match.Groups[4].Value);

            if (startX == endX && startY != endY)
            {
                //Non-diagonal, Range is between Y
                var low = Math.Min(startY, endY);
                var high = Math.Max(startY, endY);
                return Enumerable.Range(low, high - low + 1).Select(y => (startX, y));
            }
            else if (startY == endY && startX != endX)
            {
                //Non-diagonal, Range is between X
                var low = Math.Min(startX, endX);
                var high = Math.Max(startX, endX);
                return Enumerable.Range(low, high - low + 1).Select(x => (x, startY));
            }

            if (!includeDiagonal)
            {
                return Array.Empty<(int, int)>();
            }

            //Diagonal
            var lowX = Math.Min(startX, endX);
            var highX = Math.Max(startX, endX);

            var xTravel = Enumerable.Range(lowX, highX - lowX + 1);
            if (startX == highX)
            {
                //Descending
                xTravel = xTravel.Reverse();
            }

            var lowY = Math.Min(startY, endY);
            var highY = Math.Max(startY, endY);
            var yTravel = Enumerable.Range(lowY, highY - lowY + 1);
            if (startY == highY)
            {
                yTravel = yTravel.Reverse();
            }

            return Enumerable.Zip(xTravel, yTravel, (x, y) => (x, y));
        }
    }
}
