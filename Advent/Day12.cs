using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Year2021
{
    using System;

    internal class Day12 : Solution
    {
        private readonly Dictionary<string, List<string>> _nodes = new Dictionary<string, List<string>>();

        private readonly List<List<string>> _paths = new List<List<string>>();

        internal Day12()
        {
            foreach (var line in File.ReadAllLines(this._path))
            {
                var elements = line.Split('-');
                var name = elements[0];
                var dest = elements[1];
                if (this._nodes.ContainsKey(name)) this._nodes[name].Add(dest);
                else this._nodes.Add(name, new List<string> { dest });
                if (this._nodes.ContainsKey(dest)) this._nodes[dest].Add(name);
                else this._nodes.Add(dest, new List<string> { name });
            }
        }

        internal void Part1()
        {
            foreach (var destination in this._nodes["start"])
            {
                this.Explore(destination, new List<string> { "start" }, part2Rules: false);
            }
            this.PrintPaths();
            Display(this._paths.Count);
        }

        internal void Part2()
        {
            foreach (var destination in this._nodes["start"])
            {
                this.Explore(destination, new List<string> { "start" }, part2Rules: true);
            }
            Display(this._paths.Count);
        }

        private void PrintPaths()
        {
            foreach (var path in this._paths)
            {
                Console.WriteLine(string.Join(",", path));
            }
        }

        private void Explore(string currentNode, List<string> path, bool part2Rules)
        {
            var currentPath = new List<string>(path) { currentNode };
            Console.WriteLine("*****************");
            Console.WriteLine(string.Join(",", currentPath));
            var nodesToExplore = part2Rules ?
                this._nodes[currentNode].Where(dest => ShouldConsiderNodePart2(dest, currentPath))
                :
                this._nodes[currentNode].Where(dest => ShouldConsiderNodePart1(dest, currentPath));
            foreach (var nextNode in nodesToExplore)
            {
                if (nextNode == "end")
                {
                    var completedPath = new List<string>(currentPath) { nextNode };
                    this._paths.Add(completedPath);
                }
                else
                {
                    Explore(nextNode, currentPath, part2Rules);
                }
            }
        }

        private bool ShouldConsiderNodePart1(string name, List<string> path)
        {
            if (IsSmallCave(name) && path.Contains(name)) return false;

            return true;
        }

        private bool ShouldConsiderNodePart2(string name, List<string> path)
        {
            if (name == "start") return false; //start node, stop
            if (!IsSmallCave(name)) return true; //big cave, continue

            var smallCavesInPath = path.Where(stop => IsSmallCave(stop));
            if (smallCavesInPath.Contains(name) == false)
            {
                //Haven't encountered this small cave before, continue
                return true;
            }
            //remove unique values from small caves in path, if any remains, we have explored a small cave twice already
            var smallCavesInPathCopy = new List<string>(smallCavesInPath);
            foreach (var distinct in smallCavesInPath.Distinct())
            {
                smallCavesInPathCopy.Remove(distinct);
            }

            return smallCavesInPathCopy.Count == 0;
        }

        private List<string> GetPart2NodesToConsider()
        {
            return null;
        }

        private static bool IsSmallCave(string name)
        {
            return name.All(c => char.IsLower(c));
        }
    }
}
