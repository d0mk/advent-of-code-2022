using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Program
{
    public static void Main()
    {
        // just an ugly expression which gives back a list of namespaces in ascending
        // order by comparing the day number (namespaces are like AdventOfCode.DayX)
        var groupedNamespaces = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .GroupBy(t => t.Namespace)
            .Where(n => n.Key.Contains("Day"))
            .OrderBy(x => Convert.ToInt32(Regex.Match(x.Key, @"Day(\d+)").Groups[1].Value));

        int dayNumber = 1;

        // also an ugly foreach which iterates over all solutions,
        // executes them and prints the results
        foreach (var typesInNamespace in groupedNamespaces)
        {
            var solutionType = typesInNamespace.FirstOrDefault(x => Regex.Match(x.FullName, @"AdventOfCode\.Day\d+\.Solution$").Success);

            var solutionInstance = Activator.CreateInstance(
                solutionType,
                $"Day{dayNumber}/input.txt");

            dynamic solutionContext = Convert.ChangeType(solutionInstance, solutionType);

            var part1Result = TryGetResult(() => solutionContext.GetSolutionPart1());
            var part2Result = TryGetResult(() => solutionContext.GetSolutionPart2());

            Console.WriteLine($"Day {dayNumber++}");
            Console.WriteLine($"* Part 1: {part1Result}\n* Part 2: {part2Result}\n");
        }
    }

    private static object? TryGetResult<T>(Func<T> method)
    {
        try
        {
            return method();
        }
        catch (NotImplementedException)
        {
            return "not implemented!";
        }
        catch (Exception ex)
        {
            return $"Unexpected error occurred: {ex.Message}";
        }
    }
}