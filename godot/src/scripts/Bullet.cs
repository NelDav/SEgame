using Godot;

/// <summary>
/// The base class of all bullets
/// </summary>
abstract public class Bullet : RigidBody2D
{ 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetContactMonitor(true);
        SetMaxContactsReported(1);
        Connect("body_entered", this, nameof(onCollision));
    }

    //Detects, if the bullet collide with something
    private string onCollision(Node body)
    {
        //creates the collision animation;
        collisionAnimation();

        //delte bullet
        GetParent().RemoveChild(this);

        //Return the players name, if the bullet collide witha player.
        if (false)
        {

        }

        return "";
    }


    //The Animation wich should be shown, if a bullet collide.
    public abstract void collisionAnimation();

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
