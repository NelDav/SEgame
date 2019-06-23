using Godot;
using System;

/// <summary>
/// Healthkit SupplyObject that restores a variable amount of health to the player.
/// </summary>
public class AmmunitionKit : SupplyObject
{
    [Export]
    public int amount;

    protected override void DoEffect(Player target)
    {
        if(target.CurrentWeapon is RangedWeapon)
        {
            ((RangedWeapon)target.CurrentWeapon).CurrentAmmunition += amount;
        }
    }
}
