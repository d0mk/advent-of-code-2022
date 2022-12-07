using AdventOfCode.Base;

namespace AdventOfCode.Day7;

public class Solution : BaseSolution<int, object>
{
    private const string TerminalSymbol = "$";
    private readonly FileSystem fileSystem = new FileSystem();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        foreach (var line in fileContent)
        {
            if (line.StartsWith(TerminalSymbol))
            {
                ResolveCommand(line, fileSystem);
                continue;
            }
            
            fileSystem.ParseLsLine(line);
        }
    }

    public override int GetSolutionPart1()
    {
        int maximumSize = 100000;
        int totalSize = 0;

        int rootSize = fileSystem.RootDirectory.GetTotalSize();

        if (rootSize <= maximumSize)
        {
            totalSize += rootSize;
        }

        foreach (var directory in GetSubDirectories(fileSystem.RootDirectory))
        {
            int directorySize = directory.GetTotalSize();

            if (directorySize <= maximumSize)
            {
                totalSize += directorySize;
            }
        }

        return totalSize;
    }

    private IEnumerable<Directory> GetSubDirectories(Directory parentDirectory)
    {
        if (parentDirectory.SubDirectories.Any())
        {
            foreach (var subDirectory in parentDirectory.SubDirectories)
            {
                foreach (var directory in GetSubDirectories(subDirectory))
                {
                    yield return directory;
                }
            }
        }
        
        yield return parentDirectory;
    }

    public override object GetSolutionPart2()
    {
        throw new NotImplementedException();
    }

    private void ResolveCommand(string line, FileSystem fileSystem)
    {
        string[] splitLine = line.Split(" ");
        string command = splitLine[1];

        switch (command)
        {
            case "cd":
                string targetDirectory = splitLine[2];
                fileSystem.ChangeDirectory(targetDirectory);
                break;

            case "ls":
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(command));
        }
    }
}