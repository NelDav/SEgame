using Godot;
using System;

public class WeaponSpawn : Position2D
{
    [Signal]
    delegate void spawnWeapon(PackedScene weapon, Vector2 location);

    [Export]
    public float spawnInterval;

    private float elapsedTime = 0;
    private bool weaponSpawned = false;

    private PackedScene weaponScene = GD.Load("res://src/scenes/weapons/SalmonWeapon.tscn") as PackedScene;

    public override void _Process(float delta)
    {
        elapsedTime += delta;

        if ((elapsedTime >= spawnInterval) && !(weaponSpawned))
        {
            activate();
            weaponSpawned = true;
        }

    }

    private void activate()
    {
        EmitSignal(nameof(spawnWeapon), weaponScene, GetPosition());
        GD.Print("Weapon Spawned");

    }

    public void reset()
    {
        elapsedTime = 0;
        weaponSpawned = false;
    }
}
