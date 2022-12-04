using AdventOfCode.Base;

namespace AdventOfCode.Day3;

public class Solution : BaseSolution<int, int>
{
    private readonly Dictionary<char, int> priorities = new();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        int priority = 1;

        for (char c = 'a'; c <= 'z'; c++)
        {
            priorities.Add(c, priority++);
        }

        for (char c = 'A'; c <= 'Z'; c++)
        {
            priorities.Add(c, priority++);
        }
    }

    public override int GetSolutionPart1()
    {
        int sumOfPriorities = 0;

        foreach (var line in fileContent)
        {
            var firstHalf = line.Substring(0, line.Length / 2);
            var secondHalf = line.Substring(line.Length / 2, line.Length / 2);

            var commonElements = Enumerable.Intersect(firstHalf, secondHalf);

            foreach (var item in commonElements)
            {
                sumOfPriorities += priorities[item];
            }
        }

        return sumOfPriorities;
    }

    public override int GetSolutionPart2()
    {
        int sumOfPriorities = 0;

        var chunkedLines = fileContent.Chunk(3);

        // TODO: refactor, slow solution
        foreach (var chunk in chunkedLines)
        {
            foreach (var item in chunk[0])
            {
                if (chunk[1].Contains(item) && chunk[2].Contains(item))
                {
                    sumOfPriorities += priorities[item];
                    break;
                }
            }
        }

        return sumOfPriorities;
    }
}