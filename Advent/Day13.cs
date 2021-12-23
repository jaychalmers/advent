namespace Year2021
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    internal class Day13 : Solution
    {
        private readonly List<FoldInstruction> _foldInstructions = new List<FoldInstruction>();

        private bool[,] _dots;

        private int _width;

        private int _height;

        internal Day13()
        {
            var lines = File.ReadAllLines(this._path);
            var dotLines = new List<string>();

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (line.StartsWith("fold along "))
                    {
                        this._foldInstructions.Add(new FoldInstruction(line));
                    }
                    else
                    {
                        dotLines.Add(line);
                    }
                }
            }

            _width = this._foldInstructions.First(ins => ins.Orientation == 'x').Position * 2 + 1;
            _height = this._foldInstructions.First(ins => ins.Orientation == 'y').Position * 2 + 1;

            this._dots = new bool[_height, _width];

            foreach (var line in dotLines)
            {
                var coords = line.Split(',');
                this._dots[int.Parse(coords[1]), int.Parse(coords[0])] = true;
            }
        }

        internal void Part1()
        {
            this.FoldBoard(this._foldInstructions.First());
            this.PrintBoard();
            Display(this.CountDots());
        }

        internal void Part2()
        {
            foreach (var fold in this._foldInstructions)
            {
                this.FoldBoard(fold);
            }
            this.PrintBoard();
        }

        private void FoldBoard(FoldInstruction instruction)
        {
            bool[,] newBoard;
            int newHeight = this._height;
            int newWidth = this._width;

            if (instruction.Orientation == 'x')
            {
                newWidth = this._width / 2;
                newBoard = new bool[this._height, newWidth];

                for (int y = 0; y < this._height; y++)
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        var foldedX = this._width - 1 - x;
                        newBoard[y, x] = this._dots[y, x] || this._dots[y, foldedX];
                    }
                }
            }
            else
            {
                newHeight = this._height / 2;
                newBoard = new bool[newHeight, this._width];

                for (int y = 0; y < newHeight; y++)
                {
                    for (int x = 0; x < this._width; x++)
                    {
                        var foldedY = this._height - 1 - y;
                        newBoard[y, x] = this._dots[y, x] || this._dots[foldedY, x];
                    }
                }
            }

            this._height = newHeight;
            this._width = newWidth;
            this._dots = newBoard;
        }

        private int CountDots()
        {
            int count = 0;
            foreach (var cell in this._dots)
            {
                if (cell) count++;
            }

            return count;
        }

        private void PrintBoard()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < this._height; y++)
            {
                for (int x = 0; x < this._width; x++)
                {
                    sb.Append(this._dots[y, x] ? "*" : ".");
                }

                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        private class FoldInstruction
        {
            internal char Orientation { get; }
            internal int Position { get; }

            internal FoldInstruction(string inputLine)
            {
                Orientation = inputLine[11];
                Position = int.Parse(inputLine.Substring(13));
            }
        }
    }
}
