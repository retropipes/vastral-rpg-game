using VastralRPG.Game.Engine.Models;
using System.Collections.Generic;
namespace VastralRPG.Game.Engine.Factories;
internal static class WorldFactory
{
    internal static World CreateWorld()
    {
        var locations = new List<Location>
            {
                new Location
                {
                    XCoordinate = -2,
                    YCoordinate = -1,
                    Name = "Farmer's Field",
                    Description = "There are rows of corn growing here, with giant rats hiding between them.",
                    ImageName = "/images/locations/farm-fields.png"
                },
                new Location
                {
                    XCoordinate = -1,
                    YCoordinate = -1,
                    Name = "Farmer's House",
                    Description = "This is the house of your neighbor, Farmer Ted.",
                    ImageName = "/images/locations/farmhouse.png",
                    TraderHere = TraderFactory.GetTraderById(102)
                },
                new Location
                {
                    XCoordinate = 0,
                    YCoordinate = -1,
                    Name = "Home",
                    Description = "This is your home.",
                    ImageName = "/images/locations/home.png"
                },
                new Location
                {
                    XCoordinate = -1,
                    YCoordinate = 0,
                    Name = "Trading Shop",
                    Description = "The shop of Susan, the trader.",
                    ImageName = "/images/locations/trader.png",
                    TraderHere = TraderFactory.GetTraderById(101)
                },
                new Location
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    Name = "Town Square",
                    Description = "You're in the center of town'.",
                    ImageName = "/images/locations/town-square.png"
                },
                new Location
                {
                    XCoordinate = 1,
                    YCoordinate = 0,
                    Name = "Town Gate",
                    Description = "There is a gate here, protecting the town from shades.",
                    ImageName = "/images/locations/town-gate.png"
                },
                new Location
                {
                    XCoordinate = 2,
                    YCoordinate = 0,
                    Name = "Haunted Forest",
                    Description = "This forest is haunted by shades.",
                    ImageName = "/images/locations/haunted-forest.png"
                },
                new Location
                {
                    XCoordinate = 0,
                    YCoordinate = 1,
                    Name = "Herbalist's Hut",
                    Description = "You see a small hut, with plants drying from the roof.",
                    ImageName = "/images/locations/herbalists-hut.png",
                    QuestsAvailableHere = new List<Quest> { QuestFactory.GetQuestById(1) },
                    TraderHere = TraderFactory.GetTraderById(103)
                },
                new Location
                {
                    XCoordinate = 0,
                    YCoordinate = 2,
                    Name = "Herbalist's Garden",
                    Description = "There are many plants here, with snakes hiding behind them.",
                    ImageName = "/images/locations/HerbalistsGarden.png"
                },
            };
        var newWorld = new World(locations);
        // add monsters at their particular location.
        newWorld.LocationAt(-2, -1).AddMonsterEncounter(2, 100);
        newWorld.LocationAt(2, 0).AddMonsterEncounter(3, 100);
        newWorld.LocationAt(0, 2).AddMonsterEncounter(1, 100);
        return newWorld;
    }
}
