namespace AdventOfCode.Interfaces;

public interface ISolution<T>
{
    T GetSolution(string inputPath);
}