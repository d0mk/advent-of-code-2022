namespace AdventOfCode;

public class Program
{
    public static void Main()
    {
        var solutionContext = new Day1.Solution(@"Day1/input.txt");

        var part1Result = solutionContext.GetSolutionPart1();
        var part2Result = solutionContext.GetSolutionPart2();

        Console.WriteLine($"Part 1: {part1Result}");
        Console.WriteLine($"Part 2: {part2Result}");
    }
}