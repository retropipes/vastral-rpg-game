using VastralRPG.Game.Engine.Actions;
using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.Services;
using System.Collections.Generic;
using System.Linq;

namespace VastralRPG.Game.Engine.Factories;

internal static class ItemFactory
{
    private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

    static ItemFactory()
    {
        BuildWeapon(1001, "Pointy Stick", 1, "1d2");
        BuildWeapon(1002, "Rusty Sword", 5, "1d3");
        BuildWeapon(1003, "Wooden Club", 11, "1d4");
        BuildWeapon(1501, "Rabbit paws", 0, "1d2");
        BuildWeapon(1502, "Turtle bite", 0, "1d3");
        BuildWeapon(1503, "Shade touch", 0, "1d4");
        BuildHealingItem(2001, "Granola bar", 5, 2);
        BuildMiscellaneousItem(3001, "Oats", 1);
        BuildMiscellaneousItem(3002, "Honey", 2);
        BuildMiscellaneousItem(3003, "Raisins", 2);
        BuildMiscellaneousItem(9001, "Rabbit tail", 1);
        BuildMiscellaneousItem(9002, "Rabbit fur", 2);
        BuildMiscellaneousItem(9003, "Turtle shell", 1);
        BuildMiscellaneousItem(9004, "Turtle egg", 2);
        BuildMiscellaneousItem(9005, "Shade mist", 1);
        BuildMiscellaneousItem(9006, "Shade ooze", 2);
    }

    public static GameItem CreateGameItem(int itemTypeID)
    {
        var standardItem = _standardGameItems.First(i => i.ItemTypeID == itemTypeID);

        return standardItem.Clone();
    }

    public static string GetItemName(int itemTypeId)
    {
        return _standardGameItems.FirstOrDefault(i => i.ItemTypeID == itemTypeId)?.Name ?? "";
    }

    private static void BuildMiscellaneousItem(int id, string name, int price)
    {
        _standardGameItems.Add(new GameItem(id, GameItem.ItemCategory.Miscellaneous, name, price));
    }

    private static void BuildWeapon(int id, string name, int price, string damageDice)
    {
        var weapon = new GameItem(id, GameItem.ItemCategory.Weapon, name, price, true);
        weapon.Action = new Attack(weapon, damageDice);
        _standardGameItems.Add(weapon);
    }

    private static void BuildHealingItem(int id, string name, int price, int hitPointsToHeal)
    {
        GameItem item = new GameItem(id, GameItem.ItemCategory.Consumable, name, price);
        item.Action = new Heal(item, hitPointsToHeal);
        _standardGameItems.Add(item);
    }
}