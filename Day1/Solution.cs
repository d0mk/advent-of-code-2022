namespace AdventOfCode.Day1;

using AdventOfCode.Interfaces;

public class Solution : ISolution<int>
{
    public int GetSolution(string inputPath)
    {
        Dictionary<int, int> elvesCaloriesDict = new();

        int currentElf = 1;
        int currentTotalCalories = 0;

        foreach (var line in File.ReadAllLines(inputPath))
        {
            if (!string.IsNullOrEmpty(line))
            {
                currentTotalCalories += Convert.ToInt32(line);
            }
            else
            {
                elvesCaloriesDict.Add(currentElf, currentTotalCalories);

                currentElf++;
                currentTotalCalories = 0;
            }
        }

        return elvesCaloriesDict.Max(x => x.Value);
    }
}