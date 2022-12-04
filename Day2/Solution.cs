using AdventOfCode.Base;

namespace AdventOfCode.Day2;

public enum GameAction { Rock = 1, Paper = 2, Scissors = 3 }

public enum RoundResult { Win = 6, Draw = 3, Loss = 0 }

public class Converter
{
    public static GameAction GameActionFromString(string symbol)
    {
        return symbol.ToUpper() switch
        {
            "X" or "A" => GameAction.Rock,
            "Y" or "B" => GameAction.Paper,
            "Z" or "C" => GameAction.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(symbol))
        };
    }

    public static RoundResult RoundResultFromGameAction(GameAction action)
    {
        return action switch
        {
            GameAction.Rock => RoundResult.Loss,
            GameAction.Paper => RoundResult.Draw,
            GameAction.Scissors => RoundResult.Win,
            _ => throw new ArgumentOutOfRangeException(nameof(action))
        };
    }
}

public class Solution : BaseSolution<int, int>
{
    private readonly List<(GameAction, GameAction)> actionsSequence = new List<(GameAction, GameAction)>();

    public Solution(string inputPath) : base(inputPath)
    {
        InitializeData();
    }

    protected override void InitializeData()
    {
        foreach (var line in fileContent)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var currentRoundActions = line.Split(" ");

            GameAction opponentAction = Converter.GameActionFromString(currentRoundActions[0]);
            GameAction myAction = Converter.GameActionFromString(currentRoundActions[1]);

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
        var updatedSequence = actionsSequence.Select(x =>
            (x.Item1, Converter.RoundResultFromGameAction(x.Item2)));

        int score = 0;

        foreach (var (opponentAction, result) in updatedSequence)
        {
            var myAction = GetActionBasedOnDesiredResult(opponentAction, result);
            score += GetPointsForCurrentRound(myAction, result);
        }

        return score;
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

    private GameAction GetActionBasedOnDesiredResult(GameAction opponentAction, RoundResult result)
    {
        // TODO: refactor
        
        if (result == RoundResult.Win)
        {
            return opponentAction switch
            {
                GameAction.Rock => GameAction.Paper,
                GameAction.Paper => GameAction.Scissors,
                GameAction.Scissors => GameAction.Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(opponentAction))
            };
        }

        if (result == RoundResult.Draw)
        {
            return opponentAction;
        }

        if (result == RoundResult.Loss)
        {
            return opponentAction switch
            {
                GameAction.Rock => GameAction.Scissors,
                GameAction.Paper => GameAction.Rock,
                GameAction.Scissors => GameAction.Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(opponentAction))
            };
        }

        throw new ArgumentOutOfRangeException(nameof(result));
    }
}