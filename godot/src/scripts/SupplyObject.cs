using Godot;
using System;

/// <summary>
/// Abstract base class for supply objects. Effects can be implemented in the DoEffect() Method.
/// </summary>
public abstract class SupplyObject : Sprite
{
    [Export]
    public float spawnInterval;

    private float elapsedTime;

    public override void _Process(float delta)
    {
        elapsedTime += delta;
        if (elapsedTime > spawnInterval)
        {
            Activate();
        }
    }

    private void Deactivate()
    {
        this.Hide();
        this.GetChild(0).SetBlockSignals(true);
        elapsedTime = 0;
    }

    private void Activate()
    {
        this.Show();
        this.GetChild(0).SetBlockSignals(false);
        foreach (PhysicsBody2D body in ((Area2D)this.GetChild(0)).GetOverlappingBodies())
        {
            _on_Area2D_body_entered(body);
        }
    }

    private void _on_Area2D_body_entered(object body)
    {
        Player interactingPlayer = (Player)body;
        DoEffect(interactingPlayer);
        Deactivate();
    }

    protected abstract void DoEffect(Player target);
}