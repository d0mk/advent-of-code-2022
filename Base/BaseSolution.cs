namespace AdventOfCode.Base;

public abstract class BaseSolution<Part1ResultType, Part2ResultType>
{
    protected string[]? fileContent;

    public BaseSolution(string inputPath)
    {
        fileContent = File.ReadAllLines(inputPath);
    }

    protected abstract void InitializeData();

    public abstract Part1ResultType GetSolutionPart1();

    public abstract Part2ResultType GetSolutionPart2();
}