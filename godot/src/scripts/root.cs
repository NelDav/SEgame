using Godot;

public class root : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("Player").Connect("Shoot", this, nameof(on_Player_Shoot));
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void on_Player_Shoot(PackedScene bullet, Vector2 location, float direction)
    {
        Bullet bulletInstance = (Bullet)bullet.Instance();
        bulletInstance.SetPosition(location);
        bulletInstance.LinearVelocity = bulletInstance.LinearVelocity.Rotated(direction);
        AddChild(bulletInstance);
        //bulletInstance.Velocity = bulletInstance.Velocity.Rotated(direction);
    }
}
