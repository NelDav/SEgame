using Godot;

public class StandardtBullet : Bullet
{
    /*public StandardtBullet(Vector2 position, double direction, double momentum)
    {
        //SetPosition(position);
    }*/

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetLinearVelocity(new Vector2(1000, 50));
        base._Ready();
    }

    public override void collisionAnimation()
    {
        GD.Print("collide!");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}