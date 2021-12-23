using System;
using System.Collections.Generic;
using System.Text;

namespace Year2021
{
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using System.Xml;

    internal class Day14 : Solution
    {
        private readonly Dictionary<string, char> _insertionRules = new Dictionary<string, char>();

        private readonly string _polymerTemplate;

        internal Day14()
        {
            var inputs = File.ReadAllLines(_path);
            this._polymerTemplate = inputs[0];

            for (int i = 2; i < inputs.Length; i++)
            {
                var criteria = inputs[i].Substring(0, 2);
                var insertion = inputs[i][6];
                this._insertionRules.Add(criteria, insertion);
            }
        }

        internal void Part1()
        {
            string result = this._polymerTemplate;

            for (int i = 0; i < 10; i++)
            {
                result = RunInsertion(result);
            }

            var charCounts = result.ToCharArray()
                .GroupBy(letter => letter)
                .OrderBy(group => group.Count());

            var lowest = charCounts.First();
            var highest = charCounts.Last();

            Display(highest.Count() - lowest.Count());
        }

        private void AddOrSet(Dictionary<string, long> dict, string key, long value)
        {
            if (dict.ContainsKey(key)) dict[key] = value;
            else dict.Add(key, value);
        }

        private void AddToValueOrZero(Dictionary<string, long> dict, string key, long value)
        {
            if (dict.ContainsKey(key)) dict[key] += value;
            else dict.Add(key, value);
        }

        internal void Part2()
        {
            var pairCounts = new Dictionary<string, long>();
            var charCounts = new Dictionary<string, long>();
            foreach (var pair in this.GetPairs())
            {
                AddToValueOrZero(pairCounts, pair, 1);
            }

            foreach (var c in this._polymerTemplate)
            {
                AddToValueOrZero(charCounts, "" + c, 1);
            }

            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine("Iteration " + i + "...");
                var pairsToReset = new List<string>();
                var pairsToInsert = new Dictionary<string, long>();
                foreach (var pair in pairCounts.Keys)
                {
                    if (this._insertionRules.Keys.Contains(pair))
                    {
                        pairsToReset.Add(pair);
                        //As many times as this pair occured
                        var pairOccurenceCount = pairCounts[pair];
                        //update char count
                        var c = "" + this._insertionRules[pair];
                        AddToValueOrZero(charCounts, c, pairOccurenceCount);

                        //note down pairs to add
                        var r1 = pair[0] + c;
                        var r2 = c + pair[1];
                        AddToValueOrZero(pairsToInsert, r1, pairOccurenceCount);
                        AddToValueOrZero(pairsToInsert, r2, pairOccurenceCount);
                    }
                }
                //reset to zero, have to do this outside foreach
                foreach (var pair in pairsToReset)
                {
                    pairCounts[pair] = 0;
                }
                //reinsert
                foreach (var pair in pairsToInsert)
                {
                    AddToValueOrZero(pairCounts, pair.Key, pair.Value);
                }

                var total = charCounts.Sum(x => x.Value);
                Console.WriteLine("String length: " + total + " chars");
            }

            var orderedCounts = charCounts.OrderBy(x => x.Value);
            var lowest = orderedCounts.First().Value;
            var highest = orderedCounts.Last().Value;

            Display(highest - lowest);
        }

        private List<string> GetPairs()
        {
            var pairs = new List<string>();

            for (int i = 0; i < this._polymerTemplate.Length - 1; i++)
            {
                pairs.Add(this._polymerTemplate.Substring(i, 2));
            }

            return pairs;
        }

        private string PairsToString(List<string> pairs)
        {
            var sb = new StringBuilder();

            foreach (var pair in pairs)
            {
                sb.Append(pair[0]);
            }

            sb.Append(pairs.Last()[1]);

           return sb.ToString();
        }

        private void RunComplexInsertion(List<string> pairs)
        {
            Console.WriteLine("Attempting to run complex insertion with " + pairs.Count + " pairs...");
            var replacements = new List<ComplexReplacement>();
            for (int i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                if (this._insertionRules.TryGetValue(pair, out char value))
                {
                    var r1 = string.Empty + pair[0] + value;
                    var r2 = string.Empty + value + pair[1];
                    replacements.Add(new ComplexReplacement(i, r1, r2));
                }
            }

            var indexModifier = 0;
            foreach (var replacement in replacements)
            {
                pairs.RemoveAt(replacement.Index + indexModifier);
                pairs.InsertRange(replacement.Index + indexModifier, new List<string>{replacement.R1, replacement.R2});
                indexModifier++;
            }
        }

        private string RunInsertion(string input)
        {
            var insertions = new List<(int, char)>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                var inspectionZone = input.Substring(i, 2);

                if (this._insertionRules.TryGetValue(inspectionZone, out char value))
                {
                    insertions.Add((i+1,value));
                }
            }

            var updatedString = input;
            var indexModifier = 0;
            foreach (var insertion in insertions)
            {
                updatedString = updatedString.Insert(insertion.Item1 + indexModifier, "" + insertion.Item2);
                indexModifier++;
            }

            return updatedString;
        }

        private class ComplexReplacement
        {
            internal int Index { get; }
            internal string R1 { get; }
            internal string R2 { get; }

            internal ComplexReplacement(int i, string r1, string r2)
            {
                Index = i;
                R1 = r1;
                R2 = r2;
            }
        }
    }
}
