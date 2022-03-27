using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.ViewModels;

public interface IGameSession
{
    Player CurrentPlayer { get; }

    void AddXP();
}