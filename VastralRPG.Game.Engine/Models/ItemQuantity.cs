using VastralRPG.Game.Engine.Factories;

namespace VastralRPG.Game.Engine.Models;

public class ItemQuantity
{
    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public string QuantityItemDescription =>
        $"{ItemFactory.GetItemName(ItemId)} (x{Quantity})";
}