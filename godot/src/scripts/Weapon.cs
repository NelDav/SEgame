using Godot;
using System;

/// <summary>
/// Abstract Weapon Class, implementing and defining functions, applying to every weapon
/// </summary>
abstract public class Weapon : Node2D
{
    public abstract double AttackSpeed { get; }


    public RigidBody2D player;

    public override void _Ready()
    {
        player = (Godot.RigidBody2D)GetParent();
    }

    public abstract void setFlip(bool flip);

    public override void _PhysicsProcess(float delta)
    {
        if (player == null)
        {
            return;
        }
        // TODO: Why do we need to set the global position?
        SetGlobalPosition(player.GetGlobalPosition());
    }
}
