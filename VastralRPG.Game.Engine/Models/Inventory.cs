namespace VastralRPG.Game.Engine.Models;

public class Inventory
{
    private readonly List<GameItem> _backingInventory = new();
    private readonly List<GroupedInventoryItem> _backingGroupedInventory = new();

    public Inventory(IEnumerable<GameItem> items)
    {
        if (items == null)
        {
            return;
        }
        foreach (GameItem item in items)
        {
            AddItem(item);
        }
    }

    public Inventory()
    {
    }

    public IReadOnlyList<GameItem> Items => _backingInventory.AsReadOnly();

    public IReadOnlyList<GroupedInventoryItem> GroupedItems => _backingGroupedInventory.AsReadOnly();

    public IList<GameItem> Weapons =>
            Items.Where(i => i.Category == GameItem.ItemCategory.Weapon).ToList();

    public List<GameItem> Consumables =>
            Items.Where(i => i.Category == GameItem.ItemCategory.Consumable).ToList();

    public void AddItem(GameItem item)
    {
        _ = item ?? throw new ArgumentNullException(nameof(item));
        _backingInventory.Add(item);
        if (item.IsUnique)
        {
            _backingGroupedInventory.Add(new GroupedInventoryItem { Item = item, Quantity = 1 });
        }
        else
        {
            if (_backingGroupedInventory.All(gi => gi.Item.ItemTypeID != item.ItemTypeID))
            {
                _backingGroupedInventory.Add(new GroupedInventoryItem { Item = item, Quantity = 0 });
            }
            _backingGroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
        }
    }

    public void RemoveItem(GameItem item)
    {
        _ = item ?? throw new ArgumentNullException(nameof(item));
        _backingInventory.Remove(item);
        if (item.IsUnique == false)
        {
            GroupedInventoryItem? groupedInventoryItemToRemove =
                _backingGroupedInventory.FirstOrDefault(gi => gi.Item.ItemTypeID == item.ItemTypeID);
            if (groupedInventoryItemToRemove != null)
            {
                if (groupedInventoryItemToRemove.Quantity == 1)
                {
                    _backingGroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else
                {
                    groupedInventoryItemToRemove.Quantity--;
                }
            }
        }
    }

    public void RemoveItems(IList<ItemQuantity> itemQuantities)
    {
        _ = itemQuantities ?? throw new ArgumentNullException(nameof(itemQuantities));
        foreach (ItemQuantity itemQuantity in itemQuantities)
        {
            for (int i = 0; i < itemQuantity.Quantity; i++)
            {
                RemoveItem(Items.First(item => item.ItemTypeID == itemQuantity.ItemId));
            }
        }
    }

    public bool HasAllTheseItems(IEnumerable<ItemQuantity> items)
    {
        return items.All(item => Items.Count(i => i.ItemTypeID == item.ItemId) >= item.Quantity);
    }
}