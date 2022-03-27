using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.ViewModels;

public interface IGameSession
{
    Player CurrentPlayer { get; }

    Location CurrentLocation { get; }

    MovementUnit Movement { get; }

    void OnLocationChanged(Location newLocation);
}