using Godot;
using System;

abstract public class Weapon : Node2D
{
    public RigidBody2D player;

    public override void _Ready()
    {
        player = (Godot.RigidBody2D)GetParent();
    }

    public abstract void setFlip(bool flip);

    public override void _PhysicsProcess(float delta)
    {
        if (player == null)
            return;
        // TODO: Why do we need to set the global position?
        SetGlobalPosition(player.GetGlobalPosition());
    }
}
