using System.IO;

namespace Year2021
{
    internal class Day2 : Solution
    {
        int pos, depth, aim;

        public void Part1()
        {
            foreach(string command in File.ReadAllLines(_path))
            {
                var commandParts = command.Split(' ');

                var instruction = commandParts[0].ToUpperInvariant();
                var value = int.Parse(commandParts[1]);

                switch (instruction)
                {
                    case "FORWARD":
                        pos = pos + value;
                        break;
                    case "DOWN":
                        depth = depth + value;
                        break;
                    case "UP":
                        depth = depth - value;
                        break;
                    default:
                        throw new System.Exception();
                }
            }

            Display(pos * depth);
        }

        public void Part2()
        {
            foreach (string command in File.ReadAllLines(_path))
            {
                var commandParts = command.Split(' ');

                var instruction = commandParts[0].ToUpperInvariant();
                var value = int.Parse(commandParts[1]);

                switch (instruction)
                {
                    case "FORWARD":
                        pos = pos + value;
                        depth = depth + aim * value;
                        break;
                    case "DOWN":
                        aim = aim + value;
                        break;
                    case "UP":
                        aim = aim - value;
                        break;
                    default:
                        throw new System.Exception();
                }
            }

            Display(pos * depth);
        }
    }
}
