using Godot;
using System;

/// <summary>
/// Abstract RangedWeapon class, defining and implementing functios applying to every ranged weapon
/// </summary>
/// 
abstract public class RangedWeapon : Weapon
{
    public abstract int MaxAmmunition { get; }
    public abstract int Precision { get; }

    private int currentAmmunition;
    public int CurrentAmmunition
    {
        get => currentAmmunition;

        set
        {
            if(value < 0)
            {
                currentAmmunition = 0;
            }
            else if(value > MaxAmmunition)
            {
                currentAmmunition = MaxAmmunition;
            }
            else
            {
                currentAmmunition = value;
            }

            //sends an signal about
            EmitSignal("AmmunitionChangeSignal", CurrentAmmunition);
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        // click to shoot bullets
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == (int)ButtonList.Left && mouseButton.Pressed)
            {
                // TODO: calculate offset from sprite image
                int offset = 80;
                Vector2 pos = GetGlobalPosition();
                Vector2 norm = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                Vector2 res = pos + norm * offset;


                shootBullet(res, Rotation);
            }
        }
    }
}
