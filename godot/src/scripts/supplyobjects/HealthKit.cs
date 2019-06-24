using Godot;
using System;

/// <summary>
/// Healthkit SupplyObject that restores a variable amount of health to the player.
/// </summary>
public class HealthKit : SupplyObject
{
    [Export]
    public int amount;

    protected override void DoEffect(Player target)
	{
		target.Health += amount;
	}
}
