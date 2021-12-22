using System;
using System.IO;
using System.Linq;

namespace Year2021
{
    internal class Day11 : Solution
    {
        private const int BOARDSIZE = 10;
        private Octopus[,] _octopi = new Octopus[BOARDSIZE, BOARDSIZE];

        internal Day11()
        {
            using (var reader = new StreamReader(_path))
            {
                for(int i = 0; i < BOARDSIZE; i++)
                {
                    for(int j = 0; j < BOARDSIZE; j++)
                    {
                        var energy = reader.Read() - '0';
                        var octopus = new Octopus(i, j, energy);
                        _octopi[i, j] = octopus;
                    }
                    _ = reader.Read(); //Line end
                }
            }
        }

        internal void Part1()
        {
            int totalFlashCount = 0;
            for (int step = 0; step < 100; step++)
            {
                Console.WriteLine($"After step {step}:");
                PrintBoard();

                //Reset and increment
                ForEachOctopus(octo =>
                {
                    octo.HasFlashed = false;
                    octo.Energy++;
                });
                totalFlashCount += RunFlashes();
                ForEachOctopus(octo =>
                {
                    if (octo.Energy == 10) octo.Energy = 0;
                });
            }
            Display(totalFlashCount);
        }

        internal void Part2()
        {
            bool allFlashed = false;
            int step = 0;

            while (!allFlashed)
            {
                Console.WriteLine($"After step {step}:");
                PrintBoard();

                ForEachOctopus(octo =>
                {
                    octo.HasFlashed = false;
                    octo.Energy++;
                });
                RunFlashes();

                ForEachOctopus(octo =>
                {
                    if (octo.Energy == 10) octo.Energy = 0;
                });

                var i = 0;
                var j = 0;
                var thisRoundHasAllFlashed = true;
                while (thisRoundHasAllFlashed && i < BOARDSIZE)
                {
                    thisRoundHasAllFlashed = _octopi[i, j].HasFlashed;
                    j++;
                    if (j == BOARDSIZE)
                    {
                        j = 0;
                        i++;
                    }
                }

                step++;
                allFlashed = thisRoundHasAllFlashed;
            }

            Display(step);
        }

        private void PrintBoard()
        {
            ForEachOctopus(octo =>
            {
                if (octo.HasFlashed)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                var symbol = octo.Energy == 10 ? "*" : octo.Energy.ToString();
                Console.Write(symbol);
                if (octo.J == BOARDSIZE - 1)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                }
            });
            Console.WriteLine();
        }

        private int RunFlashes()
        {
            int totalFlashCount = 0;
            bool tryAgain = true;
            while (tryAgain)
            {
                var flashCount = 0;
                ForEachOctopus(octopus =>
                {
                    if (octopus.Energy == 10 && octopus.HasFlashed == false)
                    {
                        flashCount++;
                        octopus.HasFlashed = true;
                        foreach (var points in Util.GetGridPointsToCompareTo(octopus.I, octopus.J, BOARDSIZE - 1, BOARDSIZE - 1, includeDiagonal: true))
                        {
                            var octoToConsider = _octopi[points.Item1, points.Item2];
                            if (!octoToConsider.HasFlashed && octoToConsider.Energy < 10)
                            {
                                octoToConsider.Energy++;
                            }
                        }
                    }
                });
                totalFlashCount += flashCount;
                tryAgain = flashCount != 0;
            }
            return totalFlashCount;
        }

        private void ForEachOctopus(Action<Octopus> action)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var octo = _octopi[i,j];
                    action(octo);
                }
            }
        }

        private class Octopus
        {
            internal int I { get; }
            internal int J { get; }
            internal int Energy { get; set; }
            internal bool HasFlashed { get; set; }

            internal Octopus(int i, int j, int initialEnergy)
            {
                I = i;
                J = j;
                Energy = initialEnergy;
                HasFlashed = false;
            }
        }
    }
}
