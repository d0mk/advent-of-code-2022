namespace AdventOfCode.Day7;

public class FileSystem
{
    private const string RootDirectoryName = "/";
    private const string OneLevelUp = "..";

    public FileSystem()
    {
        RootDirectory = new Directory(RootDirectoryName);
        CurrentWorkingDirectory = RootDirectory;
    }

    public Directory RootDirectory { get; }

    public Directory CurrentWorkingDirectory { get; private set; }

    public void ChangeDirectory(string targetDirectory)
    {
        switch (targetDirectory)
        {
            case RootDirectoryName:
                CurrentWorkingDirectory = RootDirectory;
                break;
            
            case OneLevelUp:
                CurrentWorkingDirectory = CurrentWorkingDirectory.ParentDirectory ?? RootDirectory;
                break;

            default:
                CurrentWorkingDirectory = CurrentWorkingDirectory.SubDirectories.FirstOrDefault(x => x.Name == targetDirectory);
                break;
        }
    }

    public void ParseLsLine(string line)
    {
        var splitLine = line.Split(" ");

        string first = splitLine[0];
        string fileOrDirName = splitLine[1];

        if (first == "dir")
        {
            CreateDirectory(fileOrDirName);
            return;
        }

        if (int.TryParse(first, out int fileSize))
        {
            CreateFile(fileOrDirName, fileSize);
            return;
        }

        throw new ArgumentException(nameof(line));
    }

    private void CreateDirectory(string name)
    {
        CurrentWorkingDirectory.SubDirectories.Add(new Directory(name, CurrentWorkingDirectory));
    }

    private void CreateFile(string name, int size)
    {
        CurrentWorkingDirectory.Files.Add(new File(name, size));
    }
}

public class Directory
{
    public Directory(string name, Directory? parentDirectory = null)
    {
        Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
        ParentDirectory = parentDirectory;
    }

    public Directory? ParentDirectory { get; }

    public List<Directory> SubDirectories { get; } = new();

    public List<File> Files { get; } = new();

    public string Name { get; }

    public int GetTotalSize() => Files.Sum(x => x.Size) + SubDirectories.Sum(x => x.GetTotalSize());
}

public class File
{
    public File(string name, int size)
    {
        Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
        Size = size >= 0 ? size : throw new ArgumentNullException(nameof(size));
    }

    public string Name { get; }

    public int Size { get; }
}