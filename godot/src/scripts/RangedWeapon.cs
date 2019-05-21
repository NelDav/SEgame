using Godot;
using System;


abstract public class RangedWeapon : Weapon
{

    [Signal] delegate void shoot(PackedScene bullet, Vector2 location, float direction);
    private PackedScene bulletscene = GD.Load("res://src/scenes/bullets/StandardtBullet.tscn") as PackedScene;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        // click to shoot bullets
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == (int)ButtonList.Left && mouseButton.Pressed)
            {
                // TODO: calculate offset from sprite image
                Vector2 offset = new Vector2(40, 1);
                Vector2 pos = GetPosition();
                Transform2D transform = GetTransform();
                GD.Print(transform.Translated(offset));

                EmitSignal(nameof(shoot), bulletscene, Position, Rotation); 
            }
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        float angleRad = GetGlobalMousePosition().AngleToPoint(GetGlobalPosition());

        // get angle of mouse to weapon, to flip sprite vertically
        if (angleRad > -1.5 && angleRad < 1.5)
            setFlip(false);
        else
            setFlip(true);

        // rotate weapon
        SetGlobalRotation(angleRad);
    }
}
