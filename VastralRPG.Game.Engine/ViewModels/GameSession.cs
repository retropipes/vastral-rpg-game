using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.ViewModels;

public class GameSession : IGameSession
{
    public Player CurrentPlayer { get; set; }

    public Location CurrentLocation { get; private set; }

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
        this.CurrentLocation = new Location
        {
            Name = "Home",
            XCoordinate = 0,
            YCoordinate = -1,
            Description = "This is your house.",
            ImageName = "/images/locations/Home.png"
        };
    }

    public void AddXP()
    {
        this.CurrentPlayer.ExperiencePoints += 10;
    }
}