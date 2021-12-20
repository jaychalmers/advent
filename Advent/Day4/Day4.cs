using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Year2021
{
    internal class Day4 : Solution
    {
        private List<int> _answers;
        private List<Bingo> _boards;

        internal Day4()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            _boards = new List<Bingo>();

            using (var sr = new StreamReader(_path))
            {
                _answers = sr.ReadLine().Split(',').Select(x => Int32.Parse(x)).ToList();

                while (!sr.EndOfStream)
                {
                    _ = sr.ReadLine(); //Blank line

                    var values = new List<int>();
                    for (int i = 0; i < 5; i++)
                    {
                        values.AddRange(sr.ReadLine().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)));
                    }
                    _boards.Add(new Bingo(values));
                }
            }
        }

        internal void Part1()
        {
            Bingo winner = null;
            int currentAnswer = 0;
            while (winner == null)
            {
                int answer = _answers[currentAnswer];
                foreach(var board in _boards)
                {
                    if (board.Mark(answer))
                    {
                        winner = board;
                        var result = winner.GetScore() * answer;
                        Display(result);
                    }
                }
                currentAnswer++;
            }
        }

        internal void Part2()
        {
            Bingo lastWinner = null;
            int lastAnswer = 0;

            foreach(var answer in _answers)
            {
                var winners = new List<Bingo>();
                foreach(var board in _boards)
                {
                    if (board.Mark(answer))
                    {
                        winners.Add(board);
                        lastWinner = board;
                        lastAnswer = answer;
                    }
                }
                foreach(var board in winners)
                {
                    _boards.Remove(board);
                }
            }
            Display(lastWinner.GetScore() * lastAnswer);
        }
    }

    internal class Bingo
    {
        private static int _num = 0;

        Tile[,] _board;
        public string Id { get; }

        internal Bingo(List<int> values)
        {
            Id = "Board" + _num++;
            _board = new Tile[5,5];

            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var listIndex = 5 * i + j;
                    _board[i,j] = new Tile() { Value = values[listIndex], Marked = false };
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Id);
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    Tile t = _board[i, j];
                    var symbol = t.Marked ? string.Concat(t.Value.ToString(), "*") : t.Value.ToString();
                    sb.Append(string.Format("{0,-4}", symbol));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        internal bool Mark(int value)
        {
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (_board[i,j].Value == value)
                    {
                        _board[i,j].Marked = true;
                        if (CheckRow(i)) return true;
                        if (CheckCol(j)) return true;
                        //Does this return too early? What if there are more to mark, does that affect ifnal score?
                    }
                }
            }
            return false;
        }

        internal int GetScore()
        {
            int total = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!_board[i,j].Marked)
                    {
                        total += _board[i,j].Value;
                    }
                }
            }
            return total;
        }

        private bool CheckRow(int rowIndex)
        {
            for(int i = 0; i < 5; i++)
            {
                if (!_board[rowIndex,i].Marked) return false;
            }
            return true;
        }

        private bool CheckCol(int colIndex)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!_board[i,colIndex].Marked) return false;
            }
            return true;
        }

        internal struct Tile
        {
            internal int Value { get; set; }
            internal bool Marked { get; set; }
        }
    }
}
