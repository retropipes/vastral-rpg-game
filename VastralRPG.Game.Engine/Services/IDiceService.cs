using d20Tek.DiceNotation;
using d20Tek.DiceNotation.DieRoller;
using d20Tek.DiceNotation.Results;

namespace VastralRPG.Game.Engine.Services;

public interface IDiceService
{
    public enum RollerType
    {
        Random = 0,
        Crypto = 1,
        MathNet = 2
    }

    IDice Dice { get; }

    IDiceConfiguration Configuration { get; }

    IDieRollTracker? RollTracker { get; }

    void Configure(RollerType rollerType, bool enableTracker = false);

    DiceResult Roll();

    DiceResult Roll(string diceNotation);

    DiceResult Roll(int sides, int numDice = 1, int modifier = 0);
}