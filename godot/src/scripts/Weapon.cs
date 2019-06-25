using Godot;
using System;

/// <summary>
/// Abstract Weapon Class, implementing and defining functions, applying to every weapon
/// </summary>
abstract public class Weapon : RigidBody2D
{
    public abstract double AttackSpeed { get; }


    public Player player;
    private Sprite sprite;

    public override void _Ready()
    {
        if(GetParent() is Player)
        {
            player = (Player)GetParent();
        }

        sprite = (Sprite)GetNode("Sprite");
    }

    public void setFlip(bool flip)
    {
        if (sprite == null)
        {
            return;
        }
        sprite.SetFlipV(flip);
    }

    public override void _PhysicsProcess(float delta)
    {
        //base._PhysicsProcess(delta);

        if (player == null)
        {
            return;
        }
        // TODO: Why do we need to set the global position?
        SetGlobalPosition(player.GetGlobalPosition());


        float angleRad = GetGlobalMousePosition().AngleToPoint(GetGlobalPosition());

        // get angle of mouse to weapon, to flip sprite vertically
        if (angleRad > -1.5 && angleRad < 1.5)
        {
            setFlip(false);
        }
        else
        {
            setFlip(true);
        }

        // rotate weapon
        SetGlobalRotation(angleRad);
    }

    public void drop()
    {
        Weapon weapon = this;

        player.RemoveChild(weapon);

        if (GetNode("/root") == null)
            GD.Print("is null");

        GD.Print(GetTree());
        new DebugTool().getNodesOf(GetNode("/root"));
    }
}
