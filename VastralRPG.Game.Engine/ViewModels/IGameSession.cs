using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.ViewModels;

public interface IGameSession
{
    Player CurrentPlayer { get; }

    Location CurrentLocation { get; }

    Monster? CurrentMonster { get; }

    bool HasMonster { get; }

    Trader? CurrentTrader { get; }

    MovementUnit Movement { get; }

    IList<DisplayMessage> Messages { get; }

    void OnLocationChanged(Location newLocation);

    void AttackCurrentMonster(GameItem? currentWeapon);

    void ConsumeCurrentItem(GameItem? item);

    void CraftItemUsing(Recipe recipe);

    void ProcessKeyPress(KeyProcessingEventArgs args);
}