
using System;

namespace Year2021
{
    internal abstract class Solution
    {
        protected string _path;

        public Solution(Action fileReader = null)
        {
            _path = string.Concat("inputs/", this.GetType().Name, ".txt");
        }

        protected void Display(int result)
        {
            Display(result.ToString());
        }

        protected void Display(string result)
        {
            System.Console.WriteLine(result);
            System.Console.ReadKey();
        }
    }
}
