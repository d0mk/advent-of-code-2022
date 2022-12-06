using AdventOfCode.Base;

namespace AdventOfCode.Day6;

public class Solution : BaseSolution<int, int>
{
    private const int MarkerLength = 4;

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData() { }

    public override int GetSolutionPart1()
    {
        string dataStream = fileContent[0];

        if (dataStream.Length < MarkerLength)
        {
            throw new ArgumentOutOfRangeException(nameof(dataStream));
        }

        for (int i = MarkerLength; i < dataStream.Length; ++i)
        {
            if (dataStream[(i - MarkerLength)..i].Distinct().Count() == MarkerLength)
            {
                return i;
            }
        }

        return -1;
    }

    public override int GetSolutionPart2()
    {
        throw new NotImplementedException();
    }
}