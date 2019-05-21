using Godot;
using System;

abstract public class Weapon : Node2D
{
    public RigidBody2D player;

    public override void _Ready()
    {
        // TODO: Remove hardcoded Player
        player = (Godot.RigidBody2D)GetNode("../Player");
    }

    public abstract void setFlip(bool flip);

    public override void _PhysicsProcess(float delta)
    {
        if (player == null)
            return;
        SetPosition(player.GetPosition());
    }
}
