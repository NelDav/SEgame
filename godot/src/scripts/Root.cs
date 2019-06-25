using Godot;

public class Root : Node2D
{
    private PackedScene salmonWeapon = GD.Load("res://src/scenes/weapons/SalmonWeapon.tscn") as PackedScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Node weapon = GetNode("Player/Weapon");
        Position2D spawn = (Position2D)GetNode("MapScene/Spawn");
        RigidBody2D player = (RigidBody2D)GetNode("Player");

        //Set SalmonWeapon as start weapon
        ((InstancePlaceholder)weapon).ReplaceByInstance(salmonWeapon);
        player.SetPosition(spawn.GetGlobalPosition());

        weapon = GetNode("Player/Weapon");
        //Connecting the Shoot signal with the on_Player_Shoot handler.
        weapon.Connect("shoot", this, nameof(on_Player_Shoot));

        new DebugTool().getNodesOf(GetNode("/root/root/Player"));

        // Turn off the Menu Music
        var global = GetNode("/root/Global");
        var menuAudio = (AudioStreamPlayer)global.GetNode("MenuAudioStreamPlayer");
        menuAudio.Stop();
    }

    /// <summary>
    /// Handler of the Shoot signal. Creating a bullet.
    /// </summary>
    /// <param name="bullet">The bullet, wich should be created</param>
    /// <param name="location">The possiton, whre the bullet should spawn</param>
    /// <param name="direction">The rotation of the bullet</param>
    public void on_Player_Shoot(PackedScene bullet, Vector2 location, float direction)
    {
        Bullet bulletInstance = (Bullet)bullet.Instance();
        bulletInstance.SetPosition(location);
        bulletInstance.SetRotation(direction);
        bulletInstance.setShootDirection(direction);
        AddChild(bulletInstance);
    }
}
