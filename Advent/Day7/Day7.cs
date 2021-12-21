using System;
using System.IO;
using System.Linq;

namespace Year2021
{
    internal class Day7 : Solution
    {
        private int[] _crabPositions;

        internal Day7()
        {
            _crabPositions = File.ReadAllLines(_path).First().Split(',').Select(x => int.Parse(x)).ToArray();
        }

        public void Part1()
        {
            int quickestRoute = int.MaxValue;
            for (int candidatePosition = 0; candidatePosition < _crabPositions.Max(); candidatePosition++)
            {
                int totalFuelSpent = 0;
                foreach(var crabPos in _crabPositions)
                {
                    totalFuelSpent += Math.Abs(candidatePosition - crabPos);
                }
                if (totalFuelSpent < quickestRoute) quickestRoute = totalFuelSpent;
            }
            Display(quickestRoute);
        }

        public void Part2()
        {
            int quickestRoute = int.MaxValue;
            for (int candidatePosition = 0; candidatePosition < _crabPositions.Max(); candidatePosition++)
            {
                int totalFuelSpent = 0;
                foreach (var crabPos in _crabPositions)
                {
                    var distance = Math.Abs(candidatePosition - crabPos);
                    var fuelCost = (distance * (distance + 1)) / 2;
                    totalFuelSpent += fuelCost;
                }
                if (totalFuelSpent < quickestRoute) quickestRoute = totalFuelSpent;
            }
            Display(quickestRoute);
        }
    }
}
