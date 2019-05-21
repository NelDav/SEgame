using Godot;
using System;

public class StandardWeapon: RangedWeapon
{
    [Signal] delegate void shoot(PackedScene bullet, Vector2 location, float direction);
    private PackedScene bulletscene = GD.Load("res://src/scenes/bullets/StandardtBullet.tscn") as PackedScene;

    Sprite sprite;

    public override void _Ready()
    {
        base._Ready();
        sprite = (Godot.Sprite)GetNode("StandardWeaponTexture");
        DebugTool tool = new DebugTool();
        tool.getNodesOf(GetTree().GetRoot().GetNode("root"));
    }

    public override void shootBullet(Vector2 position, float rotation)
    {
        EmitSignal(nameof(shoot), bulletscene, position, rotation);
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
