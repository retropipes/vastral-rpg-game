namespace VastralRPG.Game.Engine.Models;

public class MonsterEncounter
{
    public MonsterEncounter(int monsterId, int chanceOfEncountering)
    {
        MonsterId = monsterId;
        ChanceOfEncountering = chanceOfEncountering;
    }

    public MonsterEncounter()
    {
    }

    public int MonsterId { get; set; }

    public int ChanceOfEncountering { get; set; }
}