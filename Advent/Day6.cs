using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Year2021
{
    internal class Day6 : Solution
    {
        private static int LIFESPAN = 6;
        private static int DELAY = 2;

        List<Fish> _fish;

        public Day6()
        {
            _fish = new List<Fish>(File.ReadAllLines(_path).First().Split(',').Select(x => new Fish(int.Parse(x))));
        }

        public void Part1()
        {
            for(int i = 0; i < 80; i++)
            {
                var newFishCount = 0;
                foreach(var fish in _fish)
                {
                    if (fish.Life == 0)
                    {
                        fish.Life = LIFESPAN;
                        newFishCount++;
                    }
                    else
                    {
                        fish.Life--;
                    }
                }
                _fish.AddRange(Enumerable.Range(0, newFishCount).Select(n => new Fish(LIFESPAN + DELAY)));
            }
            Display(_fish.Count);
        }

        public void Part2()
        {
            long[] fishes = new long[LIFESPAN + DELAY + 1];

            foreach (var fish in File.ReadAllLines(_path).First().Split(','))
            {
                fishes[int.Parse(fish)]++;
            }

            for (int i = 0; i < 256; i++)
            {
                var newFish = fishes[0];
                Array.Copy(fishes, 1, fishes, 0, fishes.Length - 1);
                fishes[LIFESPAN] += newFish;
                fishes[LIFESPAN + DELAY] = newFish;

            }
            Display(fishes.Sum());
        }

        private class Fish
        {
            internal int Life { get; set; }

            internal Fish(int lifespan)
            {
                Life = lifespan;
            }
        }
    }
}
