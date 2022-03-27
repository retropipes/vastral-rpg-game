namespace VastralRPG.Game.Engine.Models;

public class Weapon : GameItem
{
    public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)
            : base(itemTypeID, name, price, true)
    {
        MinimumDamage = minDamage;
        MaximumDamage = maxDamage;
    }

    public Weapon()
    {
    }

    public int MinimumDamage { get; set; }

    public int MaximumDamage { get; set; }

    public override GameItem Clone() =>
        new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage);
}
