using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Year2021
{
    internal class Day9 : Solution
    {
        private const int WIDTH = 100; //hardcode :{
        private const int HEIGHT = 100;

        int[,] _map = new int[HEIGHT, WIDTH];

        internal Day9()
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                int i = 0;
                int j = 0;

                var value = reader.Read();

                while (value != -1)
                {
                    if (value == 10)
                    {
                        i++;
                        j = 0;
                    }
                    else
                    {
                        _map[i, j] = (char)value - '0';
                        j++;
                    }
                    value = reader.Read();
                }
            }
        }

        public void Part1()
        {
            int totalRisk = 0;
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (IsLowPoint(i, j))
                    {
                        totalRisk += 1 + _map[i,j];
                    }
                }
            }
            Display(totalRisk);
        }

        public void Part2()
        {
            var basins = new List<List<(int, int)>>();
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (IsLowPoint(i, j))
                    {
                        var basinPoints = new List<(int, int)>();
                        GetBasinPoints(i, j, basinPoints);
                        basins.Add(basinPoints);
                    }
                }
            }
            var result = basins.OrderByDescending(basin => basin.Count).Take(3);
            Display(result.Aggregate(1, (acc, x) => acc * x.Count));
        }

        private void GetBasinPoints(int i, int j, List<(int, int)> visitedPoints)
        {
            if (!visitedPoints.Contains((i,j))) visitedPoints.Add((i, j));

            var pointsToExplore = GetPointsToCompareTo(i, j)
                .Where(points => visitedPoints.Contains(points) == false)
                .Where(points => _map[points.Item1, points.Item2] != 9);

            foreach(var points in pointsToExplore)
            {
                GetBasinPoints(points.Item1, points.Item2, visitedPoints);
            }
        }

        private (int,int)[] GetPointsToCompareTo(int i, int j)
        {
            var heightOuterBound = HEIGHT - 1;
            var widthOuterBound = WIDTH - 1;

            if (i == 0 && j == 0)
            {
                //Top left corner
                return new (int, int)[] { (i + 1, j), (i, j + 1) };
            }
            else if (i == heightOuterBound && j == widthOuterBound)
            {
                //Bottom right corner
                return new (int, int)[] { (i - 1, j), (i, j - 1) };
            }
            else if (i == 0 && j == widthOuterBound)
            {
                //Top right corner
                return new (int, int)[] { (i + 1, j), (i, j - 1) };
            }
            else if (i == heightOuterBound && j == 0)
            {
                //Bottom left corner
                return new (int, int)[] { (i - 1, j), (i, j + 1) };
            }
            else if (i == 0)
            {
                //Top row
                return new (int, int)[] { (i + 1, j), (i, j - 1), (i, j + 1) };
            }
            else if (i == heightOuterBound)
            {
                //Bottom row
                return new (int, int)[] { (i - 1, j), (i, j - 1), (i, j + 1) };
            }
            else if (j == 0)
            {
                //Left side
                return new (int, int)[] { (i - 1, j), (i + 1, j), (i, j + 1) };
            }
            else if (j == widthOuterBound)
            {
                //Right side
                return new (int, int)[] { (i - 1, j), (i + 1, j), (i, j - 1) };
            }
            else
            {
                return new (int, int)[] { (i - 1, j), (i + 1, j), (i, j - 1), (i, j + 1) };
            }
        }

        private bool IsLowPoint(int i, int j)
        {
            var pointsToCompare = GetPointsToCompareTo(i, j);
            var pointValues = pointsToCompare.Select(points => _map[points.Item1, points.Item2]);
            return pointValues.Min() > _map[i, j];
        }
    }
}
