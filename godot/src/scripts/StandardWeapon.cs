using Godot;
using System;

public class StandardWeapon: RangedWeapon
{
    Sprite sprite;

    public override void _Ready()
    {
        base._Ready();
        sprite = (Godot.Sprite)GetNode("StandardWeaponTexture");
        DebugTool tool = new DebugTool();
        tool.getNodesOf(GetTree().GetRoot().GetNode("root"));
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
