using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Year2021
{
    internal class Day10 : Solution
    {
        private static readonly char[] OpeningSymbols = new char[] { '(', '[', '{', '<' };
        private static readonly char[] ClosingSymbols = new char[] { ')', ']', '}', '>' };

        internal void Part1()
        {
            var stack = new Stack<char>();
            var errorScore = 0;

            foreach(var line in File.ReadAllLines(_path))
            {
                foreach(var symbol in line)
                {
                    if (OpeningSymbols.Contains(symbol))
                    {
                        stack.Push(symbol);
                    }
                    else if (ClosingSymbols.Contains(symbol))
                    {
                        var expected = stack.Pop();
                        if (expected != GetCorrespondingCharacter(symbol))
                        {
                            errorScore += GetErrorScore(symbol);
                        }
                    }
                }
            }

            Display(errorScore);
        }

        internal void Part2()
        {
            var autocompleteScores = new List<long>();

            foreach (var line in File.ReadAllLines(_path))
            {
                long autocompleteScore = 0;
                var stack = new Stack<char>();
                bool isValid = true;

                foreach (var symbol in line)
                {
                    if (OpeningSymbols.Contains(symbol))
                    {
                        stack.Push(symbol);
                    }
                    else if (ClosingSymbols.Contains(symbol))
                    {
                        var expected = stack.Pop();
                        if (expected != GetCorrespondingCharacter(symbol))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    while(stack.Count > 0)
                    {
                        var symbol = GetCorrespondingCharacter(stack.Pop());
                        autocompleteScore *= 5;
                        autocompleteScore += GetAutocompleteScore(symbol);
                    }
                    autocompleteScores.Add(autocompleteScore);
                }
            }

            autocompleteScores.Sort();
            var middleScore = autocompleteScores[autocompleteScores.Count / 2];

            Display(middleScore);
        }

        private static int GetAutocompleteScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;
                default:
                    throw new Exception("Tried to get autocomplete score for unsupported character");
            }
        }

        private static int GetErrorScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    throw new Exception("Tried to get error score for unsupported character");
            }
        }

        private static char GetCorrespondingCharacter(char c)
        {
            switch (c)
            {
                case '(':
                    return ')';
                case ')':
                    return '(';
                case '[':
                    return ']';
                case ']':
                    return '[';
                case '{':
                    return '}';
                case '}':
                    return '{';
                case '<':
                    return '>';
                case '>':
                    return '<';
                default:
                    throw new Exception("Unexpected character");
            }
        }
    }
}
