using AdventOfCode.Base;

namespace AdventOfCode.Day6;

public class Solution : BaseSolution<int, int>
{
    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData() { }

    public override int GetSolutionPart1()
    {
        return GetIndexOfMarkerStart(4);
    }

    public override int GetSolutionPart2()
    {
        return GetIndexOfMarkerStart(14);
    }

    private int GetIndexOfMarkerStart(int markerLength)
    {
        string dataStream = fileContent[0];

        if (dataStream.Length < markerLength)
        {
            throw new ArgumentOutOfRangeException(nameof(dataStream));
        }

        for (int i = markerLength; i < dataStream.Length; ++i)
        {
            if (dataStream[(i - markerLength)..i].Distinct().Count() == markerLength)
            {
                return i;
            }
        }

        return -1;
    }
}