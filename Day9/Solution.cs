using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Base;

namespace AdventOfCode.Day9;

enum Direction { Up, Down, Left, Right }

public struct Point : IEquatable<Point>, IEqualityComparer<Point>
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public bool Equals(Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public bool Equals(Point other)
    {
        return Equals(this, other);
    }

    public int GetHashCode([DisallowNull] Point obj)
    {
        return string.GetHashCode($"{X}{Y}");
    }
}

public class RopeKnot
{
    protected Point position;

    public Point GetCurrentPosition()
    {
        return position;
    }
}

public class Head : RopeKnot
{
    private readonly Tail tail;

    public Head(int x, int y)
    {
        position = new Point(x, y);
        tail = new Tail(this);
    }

    public void Move(int xStep, int yStep)
    {
        Point oldHeadPosition = position;

        position.X += xStep;
        position.Y += yStep;

        tail.TryUpdatePosition(position, oldHeadPosition);
    }

    public Point GetTailPosition()
    {
        return tail.GetCurrentPosition();
    }
}

public class Tail : RopeKnot
{
    public Tail(Head head)
    {
        Point headPosition = head.GetCurrentPosition();
        position = new Point(headPosition.X, headPosition.Y);
    }

    public void TryUpdatePosition(Point newPosition, Point oldHeadPosition)
    {
        // two identical points, skip
        if (position.Equals(newPosition))
        {
            return;
        }

        // newPosition is only one cell away, skip
        if (position.X == newPosition.X && Math.Abs(position.Y - newPosition.Y) == 1)
        {
            return;
        }

        // newPosition is only one cell away, skip
        if (position.Y == newPosition.Y && Math.Abs(position.X - newPosition.X) == 1)
        {
            return;
        }

        // newPosition is adjacent diagonally, skip
        if (Math.Abs(position.X - newPosition.X) == 1 && Math.Abs(position.Y - newPosition.Y) == 1)
        {
            return;
        }

        position = oldHeadPosition;
    }
}

public class Solution : BaseSolution<int, int>
{
    private readonly List<(Direction, int)> moveInstructions = new();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        foreach (string line in fileContent)
        {
            string[] splitLine = line.Split(" ");

            Direction parsedDirection = ParseDirectionCode(splitLine[0]);
            int steps = Convert.ToInt32(splitLine[1]);

            moveInstructions.Add((parsedDirection, steps));
        }
    }

    public override int GetSolutionPart1()
    {
        HashSet<Point> visitedCoordinates = new();

        Head head = new Head(0, 0);

        visitedCoordinates.Add(head.GetTailPosition());

        foreach (var (direction, steps) in moveInstructions)
        {
            for (int stepCount = 0; stepCount < steps; ++stepCount)
            {
                switch (direction)
                {
                    case Direction.Left:
                        head.Move(-1, 0);
                        break;

                    case Direction.Right:
                        head.Move(+1, 0);
                        break;

                    case Direction.Up:
                        head.Move(0, +1);
                        break;

                    case Direction.Down:
                        head.Move(0, -1);
                        break;
                }

                visitedCoordinates.Add(head.GetTailPosition());
            }
        }

        return visitedCoordinates.Count;
    }

    public override int GetSolutionPart2()
    {
        throw new NotImplementedException();
    }

    private Direction ParseDirectionCode(string code)
    {
        return code switch
        {
            "R" => Direction.Right,
            "L" => Direction.Left,
            "U" => Direction.Up,
            "D" => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(code))
        };
    }
}