using Godot;
using System;

public class HealthKit : SupplyObject
{
    protected override void DoEffect(Player target)
	{
		target.Health += 10;
	}
}
