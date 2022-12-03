namespace AdventOfCode.Base;

public abstract class BaseSolution<Part1ResultType, Part2ResultType>
{
    protected string[]? fileContent;

    protected virtual void InitializeData(string inputPath) => throw new NotImplementedException();

    public virtual Part1ResultType GetSolutionPart1() => throw new NotImplementedException();

    public virtual Part2ResultType GetSolutionPart2() => throw new NotImplementedException();
}