using AdventOfCode.Base;

namespace AdventOfCode.Day2;

public enum GameAction { Rock = 1, Paper = 2, Scissors = 3 }

public enum RoundResult { Win = 6, Draw = 3, Loss = 0 }

public class Converter
{
    public static GameAction ConvertFromString(string symbol)
    {
        return symbol.ToUpper() switch
        {
            "X" or "A" => GameAction.Rock,
            "Y" or "B" => GameAction.Paper,
            "Z" or "C" => GameAction.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(symbol))
        };
    }
}

public class Solution : BaseSolution<int, int>
{
    private readonly List<(GameAction, GameAction)> actionsSequence = new List<(GameAction, GameAction)>();

    public Solution(string inputPath)
    {
        InitializeData(inputPath);
    }

    protected override void InitializeData(string inputPath)
    {
        fileContent = File.ReadAllLines(inputPath);

        foreach (var line in fileContent)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var currentRoundActions = line.Split(" ");

            GameAction opponentAction = Converter.ConvertFromString(currentRoundActions[0]);
            GameAction myAction = Converter.ConvertFromString(currentRoundActions[1]);

            actionsSequence.Add((opponentAction, myAction));
        }
    }

    public override int GetSolutionPart1()
    {
        int score = 0;

        foreach (var (opponentAction, myAction) in actionsSequence)
        {
            RoundResult result = GetMyRoundResult(opponentAction, myAction);
            score += GetPointsForCurrentRound(myAction, result);
        }

        return score;
    }

    public override int GetSolutionPart2()
    {
        throw new NotImplementedException();
    }

    private RoundResult GetMyRoundResult(GameAction opponent, GameAction me)
    {
        // TODO: refactor, disgusting

        if (me == GameAction.Rock)
        {
            return opponent switch
            {
                GameAction.Rock => RoundResult.Draw,
                GameAction.Paper => RoundResult.Loss,
                GameAction.Scissors => RoundResult.Win,
                _ => throw new ArgumentOutOfRangeException(nameof(opponent))
            };
        }

        if (me == GameAction.Paper)
        {
            return opponent switch
            {
                GameAction.Rock => RoundResult.Win,
                GameAction.Paper => RoundResult.Draw,
                GameAction.Scissors => RoundResult.Loss,
                _ => throw new ArgumentOutOfRangeException(nameof(opponent))
            };
        }

        if (me == GameAction.Scissors)
        {
            return opponent switch
            {
                GameAction.Rock => RoundResult.Loss,
                GameAction.Paper => RoundResult.Win,
                GameAction.Scissors => RoundResult.Draw,
                _ => throw new ArgumentOutOfRangeException(nameof(opponent))
            };
        }

        throw new ArgumentOutOfRangeException(nameof(me));
    }

    private int GetPointsForCurrentRound(GameAction action, RoundResult result)
    {
        return (int)action + (int)result;
    }
}