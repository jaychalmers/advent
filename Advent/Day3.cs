using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Year2021
{
    internal class Day3 : Solution
    {
        private int _inputLength;
        private string[] _input;

        public Day3()
        {
            _input = File.ReadAllLines(_path);
            _inputLength = _input[0].Length;
        }

        private static Counts GetCountsFor(IEnumerable<string> inputs, int index)
        {
            var counts = new Counts();

            foreach (string input in inputs)
            {
                counts.CountChar(input[index]);
            }

            return counts;
        }

        public void Part1()
        {
            var gammaSb = new StringBuilder();
            var epsilonSb = new StringBuilder();

            for(int i = 0; i < _inputLength; i++)
            {
                var counts = GetCountsFor(_input, i);
                gammaSb.Append(counts.MostCommon);
                epsilonSb.Append(counts.LessCommon);
            }

            int gamma = Convert.ToInt32(gammaSb.ToString(), 2);
            int epsilon = Convert.ToInt32(epsilonSb.ToString(), 2);

            Display(gamma * epsilon);
        }

        public void Part2()
        {
            string oxygenValue = GetInputForBitCriteria('1', counts => counts.MostCommon);
            string c02Value = GetInputForBitCriteria('0', counts => counts.LessCommon);

            int oxygen = Convert.ToInt32(oxygenValue, 2);
            int c02 = Convert.ToInt32(c02Value, 2);

            Display(oxygen * c02);
        }

        private string GetInputForBitCriteria(char defaultIfEven, Func<Counts, char> predicate)
        {
            var filteredInput = new List<string>(_input);

            for (int i = 0; i < _inputLength; i++)
            {
                var counts = GetCountsFor(filteredInput, i);

                filteredInput.RemoveAll(line =>
                {
                    if (counts.IsEven)
                    {
                        return line[i] == defaultIfEven;
                    }
                    return line[i] == predicate(counts);
                });

                if (filteredInput.Count == 1)
                {
                    return filteredInput.First();
                }
            }

            throw new Exception("Couldn't find it?");
        }

        private class Counts
        {
            public int Zeroes = 0;
            public int Ones = 0;

            public bool IsEven => Zeroes == Ones;

            public char MostCommon => Zeroes > Ones ? '0' : '1';
            public char LessCommon => Zeroes > Ones ? '1' : '0';

            public void CountChar(char c)
            {
                if (c == '0') Zeroes++;
                else if (c == '1') Ones++;
                else throw new Exception();
            }
        }
    }
}
