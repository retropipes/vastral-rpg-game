using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.Services;

namespace VastralRPG.Game.Engine.Factories;

internal static class MonsterFactory
{
    private static readonly IDiceService _service = DiceService.Instance;

    public static Monster GetMonster(int monsterID)
    {
        switch (monsterID)
        {
            case 1:
                Monster rabbit = new()
                {
                    Name = "Rabbit",
                    ImageName = "/images/monsters/rabbit.png",
                    CurrentHitPoints = 4,
                    MaximumHitPoints = 4,
                    RewardExperiencePoints = 5,
                    Gold = 1,
                    Dexterity = 11,
                    Strength = 5,
                    ArmorClass = 8,
                    CurrentWeapon = ItemFactory.CreateGameItem(1501)
                };
                AddLootItem(rabbit, 9001, 25);
                AddLootItem(rabbit, 9002, 75);
                return rabbit;
            case 2:
                Monster turtle = new()
                {
                    Name = "Turtle",
                    ImageName = "/images/monsters/turtle.png",
                    CurrentHitPoints = 7,
                    MaximumHitPoints = 7,
                    RewardExperiencePoints = 10,
                    Gold = 2,
                    Dexterity = 6,
                    Strength = 10,
                    ArmorClass = 10,
                    CurrentWeapon = ItemFactory.CreateGameItem(1503)
                };
                AddLootItem(turtle, 9005, 25);
                AddLootItem(turtle, 9006, 75);
                return turtle;
            case 3:
                Monster shade = new()
                {
                    Name = "Shade",
                    ImageName = "/images/monsters/shade.png",
                    CurrentHitPoints = 10,
                    MaximumHitPoints = 10,
                    RewardExperiencePoints = 15,
                    Gold = 4,
                    Dexterity = 12,
                    Strength = 15,
                    ArmorClass = 12,
                    CurrentWeapon = ItemFactory.CreateGameItem(1502)
                };
                AddLootItem(shade, 9003, 25);
                AddLootItem(shade, 9004, 75);
                return shade;
            default:
                throw new ArgumentOutOfRangeException(nameof(monsterID));
        }
    }

    private static void AddLootItem(Monster monster, int itemID, int percentage)
    {
        if (_service.Roll("1d100").Value <= percentage)
        {
            var item = ItemFactory.CreateGameItem(itemID);
            if (item != null)
            {
                monster.Inventory.AddItem(item);
            }
        }
    }
}