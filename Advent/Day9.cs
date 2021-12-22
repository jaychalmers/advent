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

            var pointsToExplore = Util.GetGridPointsToCompareTo(i, j, HEIGHT - 1, WIDTH - 1)
                .Where(points => visitedPoints.Contains(points) == false)
                .Where(points => _map[points.Item1, points.Item2] != 9);

            foreach(var points in pointsToExplore)
            {
                GetBasinPoints(points.Item1, points.Item2, visitedPoints);
            }
        }

        private bool IsLowPoint(int i, int j)
        {
            var pointsToCompare = Util.GetGridPointsToCompareTo(i, j, HEIGHT - 1, WIDTH - 1);
            var pointValues = pointsToCompare.Select(points => _map[points.Item1, points.Item2]);
            return pointValues.Min() > _map[i, j];
        }
    }
}
