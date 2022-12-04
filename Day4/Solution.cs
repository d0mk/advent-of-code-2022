using AdventOfCode.Base;

namespace AdventOfCode.Day4;

struct Pair
{
    public Pair(int left, int right)
    {
        Left = left;
        Right = right;
    }

    public Pair(string left, string right)
    {
        Left = Convert.ToInt32(left);
        Right = Convert.ToInt32(right);
    }

    public int Left { get; }

    public int Right { get; }

    public bool IsContainedBy(Pair other)
    {
        return Left >= other.Left && Right <= other.Right;
    }

    public bool OverlapsWith(Pair other)
    {
        return Right >= other.Left;
    }

    public bool IsBefore(Pair other)
    {
        return (Left + Right) < (other.Left + other.Right);
    }
}

public class Solution : BaseSolution<int, int>
{
    private readonly List<(Pair, Pair)> pairs = new();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        foreach (var line in fileContent)
        {
            var bothPairs = line.Split(",");
            var splitLeftPair = bothPairs[0].Split("-");
            var splitRightPair = bothPairs[1].Split("-");

            Pair leftPair = new Pair(splitLeftPair[0], splitLeftPair[1]);
            Pair rightPair = new Pair(splitRightPair[0], splitRightPair[1]);

            pairs.Add((leftPair, rightPair));
        }
    }

    public override int GetSolutionPart1()
    {
        int count = 0;

        foreach (var (leftPair, rightPair) in pairs)
        {
            if (leftPair.IsContainedBy(rightPair) || rightPair.IsContainedBy(leftPair))
            {
                count++;
            }
        }

        return count;
    }

    public override int GetSolutionPart2()
    {
        int count = 0;

        foreach (var (leftPair, rightPair) in pairs)
        {
            var (leftmostPair, rightmostPair) = leftPair.IsBefore(rightPair)
                ? (leftPair, rightPair)
                : (rightPair, leftPair);

            if (leftmostPair.OverlapsWith(rightmostPair))
            {
                count++;
            }
        }

        return count;
    }
}