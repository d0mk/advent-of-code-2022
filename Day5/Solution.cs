using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Base;

namespace AdventOfCode.Day5;

public class CrateStack
{
    private readonly Stack<string> crates;

    public CrateStack(IEnumerable<string> cratesList)
    {
        crates = new Stack<string>(cratesList
            .Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x)));
    }

    public void AddCrate(string crateSymbol)
    {
        if (string.IsNullOrEmpty(crateSymbol) || string.IsNullOrWhiteSpace(crateSymbol))
        {
            return;
        }

        crates.Push(crateSymbol);
    }

    public string RemoveCrate()
    {
        return crates.Pop();
    }

    public string PeekTopCrate()
    {
        return crates.Peek();
    }

    public CrateStack Clone()
    {
        // https://stackoverflow.com/a/45200965
        var array = new string[crates.Count];
        crates.CopyTo(array, 0);
        Array.Reverse(array);
        return new CrateStack(array);
    }
}

public class Instruction
{
    public Instruction(string instructionString)
    {
        var instructionKeywords = instructionString.Split(" ");

        Operation = instructionKeywords[0];
        Quantity = Convert.ToInt32(instructionKeywords[1]);
        Source = Convert.ToInt32(instructionKeywords[3]);
        Target = Convert.ToInt32(instructionKeywords[5]);
    }

    public string Operation { get; init; }

    public int Quantity { get; init; }

    public int Source { get; init; }

    public int Target { get; init; }
}

// works only for crate stack numbers denoted by single digit
// and crate names denoted by single letter
public class Solution : BaseSolution<string, string>
{
    private readonly Dictionary<int, CrateStack> crateStacks = new();
    private readonly List<Instruction> instructions = new();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        List<string> temporaryCrateData = new();
        int index = 0;

        while (true)
        {
            if (string.IsNullOrEmpty(fileContent[index]))
            {
                break;
            }

            temporaryCrateData.Insert(0, fileContent[index++]);
        }

        foreach (var stackNumberMatch in Regex.Matches(temporaryCrateData[0], @"\d"))
        {
            string stackNumberMatchAsString = stackNumberMatch.ToString();

            int stackIndex = temporaryCrateData[0].IndexOf(stackNumberMatchAsString);
            int actualStackNumber = Convert.ToInt32(stackNumberMatchAsString);

            List<string> cratesForCurrentRow = new();

            foreach (var crateRow in temporaryCrateData.Skip(1))
            {
                cratesForCurrentRow.Add(crateRow[stackIndex].ToString());
            }

            crateStacks[actualStackNumber] = new CrateStack(cratesForCurrentRow);
        }

        for (index++; index < fileContent.Length; index++)
        {
            instructions.Add(new Instruction(fileContent[index]));
        }
    }

    public override string GetSolutionPart1()
    {
        var crateStacksCopy = crateStacks.ToDictionary(x => x.Key, x => x.Value.Clone());

        // only considers "move" operation
        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Quantity; ++i)
            {
                string crateSymbol = crateStacksCopy[instruction.Source].RemoveCrate();
                crateStacksCopy[instruction.Target].AddCrate(crateSymbol);
            }
        }

        return string.Join("", crateStacksCopy
            .OrderBy(x => x.Key)
            .Select(x => x.Value)
            .Select(stack => stack.PeekTopCrate()));
    }

    public override string GetSolutionPart2()
    {
        var crateStacksCopy = crateStacks.ToDictionary(x => x.Key, x => x.Value.Clone());

        // only considers "move" operation
        foreach (var instruction in instructions)
        {
            List<string> removedCrates = new();

            for (int i = 0; i < instruction.Quantity; ++i)
            {
                string crateSymbol = crateStacksCopy[instruction.Source].RemoveCrate();
                removedCrates.Insert(0, crateSymbol);
            }

            for (int i = 0; i < instruction.Quantity; ++i)
            {
                crateStacksCopy[instruction.Target].AddCrate(removedCrates[i]);
            }
        }

        return string.Join("", crateStacksCopy
            .OrderBy(x => x.Key)
            .Select(x => x.Value)
            .Select(stack => stack.PeekTopCrate()));
    }
}
