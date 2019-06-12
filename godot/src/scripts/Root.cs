using Godot;

public class Root : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connecting the Shoot signal with the on_Player_Shoot handler.
        GetNode("Player/SalmonWeapon").Connect("shoot", this, nameof(on_Player_Shoot));
        Position2D spawn = (Position2D) GetNode("MapScene/Spawn");
        RigidBody2D player = (RigidBody2D)GetNode("Player");
        player.SetPosition(spawn.GetGlobalPosition());
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
