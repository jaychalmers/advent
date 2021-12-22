using System.IO;
using System.Linq;

namespace Year2021
{
    class Day1 : Solution
    {
        private int[] input;
        private int previous, current, counter;

        public Day1()
        {
            var file = File.ReadAllLines(_path);
            input = file.Select(x => int.Parse(x)).ToArray();
        }

        public void Part1()
        {
            int index = 1;

            while (index < input.Length)
            {
                previous = input[index - 1];
                current = input[index];

                if (current > previous)
                {
                    counter++;
                }

                index++;
            }

            Display(counter);
        }

        public void Part2()
        {
            int index = 3;
            while (index < input.Length)
            {
                previous = input[index - 3] + input[index - 2] + input[index - 1];
                current = input[index - 2] + input[index - 1] + input[index];
                
                if (current > previous)
                {
                    counter++;
                }

                index++;
            }

            Display(counter);
        }
    }
}
