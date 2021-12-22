using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Year2021
{
    internal class Day8 : Solution
    {
        /*
        private static char[] ZeroChars = new char[] { 'a', 'b', 'c', 'e', 'f', 'g' };
        private static char[] OneChars = new char[] { 'c', 'f' };
        private static char[] TwoChars = new char[] { 'a', 'c', 'd', 'e', 'g' };
        private static char[] ThreeChars = new char[] { 'a', 'c', 'd', 'f', 'g' };
        private static char[] FourChars = new char[] { 'b', 'c', 'd', 'f' };
        private static char[] FiveChars = new char[] { 'a', 'b', 'd', 'f', 'g' };
        private static char[] SixChars = new char[] { 'a', 'b', 'd', 'e', 'f', 'g' };
        private static char[] SevenChars = new char[] { 'a', 'c', 'f' };
        private static char[] EightChars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
        private static char[] NineChars = new char[] { 'a', 'b', 'c', 'd', 'f', 'g' };
        */

        private char[] ZeroChars,
                       OneChars,
                       TwoChars,
                       ThreeChars,
                       FourChars,
                       FiveChars,
                       SixChars,
                       SevenChars,
                       EightChars,
                       NineChars;

        private IEnumerable<Entry> _entries;

        internal Day8()
        {
            _entries = File.ReadAllLines(_path).Select(line => new Entry(line));
        }

        internal void Part1()
        {
            Display(_entries.Sum(entry => entry.Output.Where(OutputIsClue).Count()));
        }

        private bool OutputIsClue(string output)
        {
            return output.Length == 2 || output.Length == 4 || output.Length == 3 || output.Length == 7;
        }

        internal void Part2()
        {
            var total = 0;

            foreach(var entry in _entries)
            {
                var signals = new List<string>(entry.Signals);

                string oneSignals = signals.First(signal => signal.Length == 2);
                signals.Remove(oneSignals);
                OneChars = oneSignals.ToCharArray();

                string fourSignals = signals.First(signal => signal.Length == 4);
                signals.Remove(fourSignals);
                FourChars = fourSignals.ToCharArray();

                string sevenSignals = signals.First(signal => signal.Length == 3);
                signals.Remove(sevenSignals);
                SevenChars = sevenSignals.ToCharArray();

                string eightSignals = signals.First(signal => signal.Length == 7);
                signals.Remove(eightSignals);
                EightChars = eightSignals.ToCharArray();

                string threeSignals = signals.First(signal =>
                {
                    return signal.Length == 5 && SignalContains(signal, sevenSignals);
                });
                signals.Remove(threeSignals);
                ThreeChars = threeSignals.ToCharArray();

                string sixSignals = signals.First(signal =>
                {
                    return signal.Length == 6 && !SignalContains(signal, oneSignals);
                });
                signals.Remove(sixSignals);
                SixChars = sixSignals.ToCharArray();

                string beSignals = SignalWithout(eightSignals, threeSignals);
                string nineSignals = signals.First(signal =>
                {
                    return signal.Length == 6 && !SignalContains(signal, beSignals);
                });
                signals.Remove(nineSignals);
                NineChars = nineSignals.ToCharArray();

                string eSignals = SignalWithout(beSignals, fourSignals);

                string twoSignals = signals.First(signal =>
                {
                    return signal.Length == 5 && SignalContains(signal, eSignals);
                });
                signals.Remove(twoSignals);
                TwoChars = twoSignals.ToCharArray();

                string fiveSignals = signals.First(signal =>
                {
                    //Should be the only one remianing now
                    return signal.Length == 5;
                });
                signals.Remove(fiveSignals);
                FiveChars = fiveSignals.ToCharArray();

                string zeroSignals = signals.First(signal =>
                {
                    //Should be the only one remaining now
                    return signal.Length == 6;
                });
                signals.Remove(zeroSignals);
                ZeroChars = zeroSignals.ToCharArray();

                SortCharDefinitions();

                var sb = new StringBuilder();
                foreach(var sig in entry.Signals)
                {
                    sb.Append($"{GetDisplayNumber(sig)} ");
                }
                sb.Append("| ");
                foreach(var outp in entry.Output)
                {
                    sb.Append($"{GetDisplayNumber(outp)} ");
                }

                sb.Append($"+ {total} = ");

                var outputNumber = int.Parse(string.Format("{0}{1}{2}{3}",
                    GetDisplayNumber(entry.Output[0]),
                    GetDisplayNumber(entry.Output[1]),
                    GetDisplayNumber(entry.Output[2]),
                    GetDisplayNumber(entry.Output[3])));
                total += outputNumber;

                sb.Append(total);

                Console.WriteLine(sb.ToString());
            }

            Display(total);
        }

        private void SortCharDefinitions()
        {
            Array.Sort(ZeroChars);
            Array.Sort(OneChars);
            Array.Sort(TwoChars);
            Array.Sort(ThreeChars);
            Array.Sort(FourChars);
            Array.Sort(FiveChars);
            Array.Sort(SixChars);
            Array.Sort(SevenChars);
            Array.Sort(EightChars);
            Array.Sort(NineChars);
        }

        private string SignalWithout(string signal, string otherSignal)
        {
            var signalCharArray = signal.ToCharArray();
            var otherSignalCharArray = otherSignal.ToCharArray();
            return new string(signalCharArray.Except(otherSignalCharArray).ToArray());
        }

        private bool SignalContains(string signal, string otherSignal)
        {
            var signalCharArray = signal.ToCharArray();
            var otherSignalCharArray = otherSignal.ToCharArray();
            return otherSignal.All(letter => signalCharArray.Contains(letter));
        }

        private int GetDisplayNumber(string input)
        {
            var chars = input.ToCharArray();
            Array.Sort(chars);

            if (chars.SequenceEqual(ZeroChars)) return 0;
            else if (chars.SequenceEqual(OneChars)) return 1;
            else if (chars.SequenceEqual(TwoChars)) return 2;
            else if (chars.SequenceEqual(ThreeChars)) return 3;
            else if (chars.SequenceEqual(FourChars)) return 4;
            else if (chars.SequenceEqual(FiveChars)) return 5;
            else if (chars.SequenceEqual(SixChars)) return 6;
            else if (chars.SequenceEqual(SevenChars)) return 7;
            else if (chars.SequenceEqual(EightChars)) return 8;
            else if (chars.SequenceEqual(NineChars)) return 9;

            throw new Exception();
        }

        private class Entry
        {
            public string[] Signals { get; }
            public string[] Output { get; }

            internal Entry(string input)
            {
                var elements = input.Split(" | ");
                Signals = elements[0].Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                Output = elements[1].Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
