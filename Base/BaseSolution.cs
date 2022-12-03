namespace AdventOfCode.Base;

public abstract class BaseSolution<Part1ResultType, Part2ResultType>
{
    protected string[]? fileContent;

    protected abstract void InitializeData(string inputPath);

    public abstract Part1ResultType GetSolutionPart1();

    public abstract Part2ResultType GetSolutionPart2();
}