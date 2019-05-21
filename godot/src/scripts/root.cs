using Godot;

public class root : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connecting the Shoot signal with the on_Player_Shoot handler.
        GetNode("Player").Connect("shoot", this, nameof(on_Player_Shoot));
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
        bulletInstance.LinearVelocity = bulletInstance.LinearVelocity.Rotated(direction);
        AddChild(bulletInstance);
        //bulletInstance.Velocity = bulletInstance.Velocity.Rotated(direction);
    }
}
