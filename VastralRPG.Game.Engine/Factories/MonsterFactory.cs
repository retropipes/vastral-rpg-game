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
                Monster snake = new Monster
                {
                    Name = "Snake",
                    ImageName = "/images/monsters/Snake.png",
                    CurrentHitPoints = 4,
                    MaximumHitPoints = 4,
                    RewardExperiencePoints = 5,
                    Gold = 1,
                    DamageRoll = "1d2"
                };

                AddLootItem(snake, 9001, 25);
                AddLootItem(snake, 9002, 75);
                return snake;

            case 2:
                Monster rat = new Monster
                {
                    Name = "Rat",
                    ImageName = "/images/monsters/Rat.png",
                    CurrentHitPoints = 5,
                    MaximumHitPoints = 5,
                    RewardExperiencePoints = 5,
                    Gold = 1,
                    DamageRoll = "1d2"
                };

                AddLootItem(rat, 9003, 25);
                AddLootItem(rat, 9004, 75);
                return rat;

            case 3:
                Monster giantSpider = new Monster
                {
                    Name = "Giant Spider",
                    ImageName = "/images/monsters/GiantSpider.png",
                    CurrentHitPoints = 10,
                    MaximumHitPoints = 10,
                    RewardExperiencePoints = 10,
                    Gold = 3,
                    DamageRoll = "1d4"
                };

                AddLootItem(giantSpider, 9005, 25);
                AddLootItem(giantSpider, 9006, 75);
                return giantSpider;

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