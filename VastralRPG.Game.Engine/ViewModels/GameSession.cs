using VastralRPG.Game.Engine.Factories;
using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.ViewModels;

public class GameSession : IGameSession
{
    private readonly World currentWorld;

    public Player CurrentPlayer { get; set; }

    public Location CurrentLocation { get; private set; }

    public Monster? CurrentMonster { get; private set; }

    public bool HasMonster => CurrentMonster != null;

    public MovementUnit Movement { get; private set; }

    public GameSession()
    {
        this.CurrentPlayer = new Player
        {
            Name = "RetroPipes",
            CharacterClass = "Fighter",
            CurrentHitPoints = 10,
            MaximumHitPoints = 10,
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
        this.currentWorld = WorldFactory.CreateWorld();
        this.Movement = new MovementUnit(this.currentWorld);
        this.CurrentLocation = this.Movement.CurrentLocation;
        GetMonsterAtCurrentLocation();
        var pointyStick = ItemFactory.CreateGameItem(1001);
        if (pointyStick != null)
        {
            CurrentPlayer.Inventory.AddItem(pointyStick);
        }
    }

    public void OnLocationChanged(Location newLocation)
    {
        CurrentLocation = newLocation;
        GetMonsterAtCurrentLocation();
    }

    private void GetMonsterAtCurrentLocation() =>
        CurrentMonster = CurrentLocation.HasMonster() ? CurrentLocation.GetMonster() : null;
}