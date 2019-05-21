using Godot;
using System;

public class StandardWeapon: RangedWeapon
{
    Sprite sprite;

    public override void _Ready()
    {
        base._Ready();
        sprite = (Godot.Sprite)GetNode("StandardWeaponTexture");
    }
        
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    }

    public override void setFlip(bool flip)
    {
        if (sprite == null)
            return;
        sprite.SetFlipV(flip);
    }
}
