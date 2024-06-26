using VastralRPG.Game.Engine.Actions;

namespace VastralRPG.Game.Engine.Models;

public class GameItem
{
    public enum ItemCategory
    {
        Miscellaneous,
        Weapon,
        Consumable
    }

    public static readonly GameItem Empty = new();

    public GameItem(int itemTypeID, ItemCategory category, string name, int price, bool isUnique = false, IAction? action = null)
    {
        ItemTypeID = itemTypeID;
        Category = category;
        Name = name;
        Price = price;
        IsUnique = isUnique;
        Action = action;
    }

    public GameItem(int itemTypeID, ItemCategory category, string name, int price, string image, bool isUnique = false, IAction? action = null)
    {
        ItemTypeID = itemTypeID;
        Category = category;
        Name = name;
        Price = price;
        IsUnique = isUnique;
        Action = action;
        ImageName = image;
    }

    public GameItem()
    {
    }

    public int ItemTypeID { get; set; }

    public ItemCategory Category { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Price { get; set; }

    public bool IsUnique { get; set; }

    public IAction? Action { get; set; }

    public string ImageName { get; set; } = string.Empty;

    public virtual GameItem Clone() =>
        new(ItemTypeID, Category, Name, Price, IsUnique, Action);

    public DisplayMessage PerformAction(LivingEntity actor, LivingEntity target)
    {
        if (Action is null)
        {
            throw new InvalidOperationException("CurrentWeapon.Action cannot be null");
        }

        return Action.Execute(actor, target);
    }
}
