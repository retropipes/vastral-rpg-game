using VastralRPG.Game.Engine.Models;
using System.Collections.Generic;
using System.Linq;

namespace VastralRPG.Game.Engine.Factories;

internal static class TraderFactory
{
    private static readonly List<Trader> _traders = new();

    static TraderFactory()
    {
        _traders.Add(CreateTrader(101, "Susan"));
        _traders.Add(CreateTrader(102, "Farmer Ted"));
        _traders.Add(CreateTrader(103, "Pete the Herbalist"));
    }

    public static Trader GetTraderById(int id) => _traders.First(t => t.Id == id);

    private static Trader CreateTrader(int id, string name)
    {
        Trader t = new()
        {
            Id = id,
            Name = name,
            Level = 0,
            Gold = 100,
            MaximumHitPoints = 999,
            CurrentHitPoints = 999
        };
        var item = ItemFactory.CreateGameItem(1001);
        if (item != null)
        {
            t.Inventory.AddItem(item);
        }
        return t;
    }
}