using Godot;

public class Root : Node2D
{
    private PackedScene salmonWeapon = GD.Load("res://src/scenes/weapons/SalmonWeapon.tscn") as PackedScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Position2D spawn = (Position2D)GetNode("MapScene/Spawn");
        Player player = (Player)GetNode("Player");

        SalmonWeapon weapon = (SalmonWeapon)salmonWeapon.Instance();
        player.changeWeapon(weapon);

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
