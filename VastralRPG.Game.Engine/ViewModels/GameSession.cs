using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.ViewModels;

public class GameSession
{
    public Player CurrentPlayer { get; set; }

    public GameSession()
    {
        this.CurrentPlayer = new Player
        {
            Name = "RetroPipes",
            CharacterClass = "Fighter",
            HitPoints = 10,
            Gold = 1000,
            ExperiencePoints = 0,
            Level = 1
        };
    }

    public void AddXP()
    {
        this.CurrentPlayer.ExperiencePoints += 10;
    }
}