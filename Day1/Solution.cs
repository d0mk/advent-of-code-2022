namespace AdventOfCode.Day1;

using AdventOfCode.Base;

public class Solution : BaseSolution<int, int>
{
    private readonly Dictionary<int, IEnumerable<int>> elvesCaloriesGrouped = new();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        int startIndex = 0;
        int chunkIndex = 0;
        int elfNumber = 0;

        while (true)
        {
            chunkIndex = Array.IndexOf(fileContent, "", startIndex);

            if (chunkIndex < 0)
            {
                break;
            }
            
            var caloriesForCurrentElf = fileContent
                .Take(new Range(startIndex, chunkIndex))
                .Select(x => Convert.ToInt32(x));

            elvesCaloriesGrouped.Add(elfNumber++, caloriesForCurrentElf);

            startIndex = chunkIndex + 1;
        }
    }

    public override int GetSolutionPart1()
    {
        return elvesCaloriesGrouped.Max(x => x.Value.Sum());
    }

    public override int GetSolutionPart2()
    {
        var summedCaloriesPerElf = elvesCaloriesGrouped.Select(x => x.Value.Sum());

        return summedCaloriesPerElf
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }
}