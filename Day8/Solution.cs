using AdventOfCode.Base;

namespace AdventOfCode.Day8;

public class Solution : BaseSolution<int, object>
{
    private readonly int[,] treesGrid;

    public Solution(string inputPath) : base(inputPath)
    {
        int numberOfColumns = fileContent[0].Length;
        int numberOfRows = fileContent.Length;

        treesGrid = new int[numberOfRows, numberOfColumns];

        InitializeData();
    }

    protected override void InitializeData()
    {
        for (int rowIndex = 0; rowIndex < fileContent.Length; ++rowIndex)
        {
            for (int columnIndex = 0; columnIndex < fileContent[rowIndex].Length; ++columnIndex)
            {
                treesGrid[rowIndex, columnIndex] = Convert.ToInt32(fileContent[rowIndex][columnIndex]);
            }
        }
    }

    public override int GetSolutionPart1()
    {
        // all trees on the edges are visible
        // (this could be also "size_of_the_array * 4 - 4" if we'd be sure that the array is of size NxN)
        int countOfVisibleTrees = treesGrid.GetLength(0) * 2 + treesGrid.GetLength(1) * 2 - 4;

        for (int rowIndex = 1; rowIndex < treesGrid.GetLength(0) - 1; ++rowIndex)
        {
            for (int columnIndex = 1; columnIndex < treesGrid.GetLength(1) - 1; ++columnIndex)
            {
                if (IsVisibleFromAtLeastOneSide(rowIndex, columnIndex))
                {
                    countOfVisibleTrees++;
                }
            }
        }

        return countOfVisibleTrees;
    }

    public override object GetSolutionPart2()
    {
        int maxScenicScore = int.MinValue;

        for (int rowIndex = 1; rowIndex < treesGrid.GetLength(0) - 1; ++rowIndex)
        {
            for (int columnIndex = 1; columnIndex < treesGrid.GetLength(1) - 1; ++columnIndex)
            {
                int scenicScore = GetScenicScoreForGivenTree(rowIndex, columnIndex);

                if (scenicScore > maxScenicScore)
                {
                    maxScenicScore = scenicScore;
                }
            }
        }

        return maxScenicScore;
    }

    private bool IsVisibleFromAtLeastOneSide(int rowIndex, int columnIndex)
    {
        bool isVisible = true;

        // from left
        for (int i = 0; i < columnIndex; ++i)
        {
            if (treesGrid[rowIndex, i] >= treesGrid[rowIndex, columnIndex])
            {
                isVisible = false;
                break;
            }
        }
        
        if (!isVisible)
        {
            isVisible = true;

            // from right
            for (int i = columnIndex + 1; i < treesGrid.GetLength(1); ++i)
            {
                if (treesGrid[rowIndex, i] >= treesGrid[rowIndex, columnIndex])
                {
                    isVisible = false;
                    break;
                }
            }
        }

        if (!isVisible)
        {
            isVisible = true;

            // from top
            for (int i = 0; i < rowIndex; ++i)
            {
                if (treesGrid[i, columnIndex] >= treesGrid[rowIndex, columnIndex])
                {
                    isVisible = false;
                    break;
                }
            }
        }

        if (!isVisible)
        {
            isVisible = true;

            // from bottom
            for (int i = rowIndex + 1; i < treesGrid.GetLength(0); ++i)
            {
                if (treesGrid[i, columnIndex] >= treesGrid[rowIndex, columnIndex])
                {
                    isVisible = false;
                    break;
                }
            }
        }

        return isVisible;
    }

    private int GetScenicScoreForGivenTree(int rowIndex, int columnIndex)
    {
        return CountLeft(rowIndex, columnIndex) * CountRight(rowIndex, columnIndex)
            * CountTop(rowIndex, columnIndex) * CountBottom(rowIndex, columnIndex);
    }

    private int CountLeft(int rowIndex, int columnIndex)
    {
        int count = 0;
        
        for (int i = 1; i < treesGrid.GetLength(1); ++i)
        {
            if (columnIndex - i >= 0)
            {
                if (treesGrid[rowIndex, columnIndex - i] < treesGrid[rowIndex, columnIndex])
                {
                    count++;
                }
                else
                {
                    return count + 1;
                }
            }
        }

        return count;
    }

    private int CountRight(int rowIndex, int columnIndex)
    {
        int count = 0;
        
        for (int i = 1; i < treesGrid.GetLength(1); ++i)
        {
            if (columnIndex + i < treesGrid.GetLength(1))
            {
                if (treesGrid[rowIndex, columnIndex + i] < treesGrid[rowIndex, columnIndex])
                {
                    count++;
                }
                else
                {
                    return count + 1;
                }
            }
        }

        return count;
    }

    private int CountTop(int rowIndex, int columnIndex)
    {
        int count = 0;
        
        for (int i = 1; i < treesGrid.GetLength(0); ++i)
        {
            if (rowIndex - i >= 0)
            {
                if (treesGrid[rowIndex - i, columnIndex] < treesGrid[rowIndex, columnIndex])
                {
                    count++;
                }
                else
                {
                    return count + 1;
                }
            }
        }

        return count;
    }

    private int CountBottom(int rowIndex, int columnIndex)
    {
        int count = 0;
        
        for (int i = 1; i < treesGrid.GetLength(0); ++i)
        {
            if (rowIndex + i < treesGrid.GetLength(0))
            {
                if (treesGrid[rowIndex + i, columnIndex] < treesGrid[rowIndex, columnIndex])
                {
                    count++;
                }
                else
                {
                    return count + 1;
                }
            }
        }

        return count;
    }
}