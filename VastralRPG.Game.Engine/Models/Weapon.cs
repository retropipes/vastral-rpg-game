namespace VastralRPG.Game.Engine.Models;

public class Weapon : GameItem
{
    public Weapon(int itemTypeID, string name, int price, string damageRoll)
        : base(itemTypeID, name, price, true)
    {
        DamageRoll = damageRoll;
    }

    public Weapon()
    {
    }

    public string DamageRoll { get; set; } = string.Empty;

    public override GameItem Clone() =>
        new Weapon(ItemTypeID, Name, Price, DamageRoll);
}
