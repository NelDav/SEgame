using Godot;

/// <summary>
/// The base class of all bullets
/// </summary>
abstract public class Bullet : RigidBody2D
{
    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        SetContactMonitor(true);
        SetMaxContactsReported(1);
        SetCollisionLayer(LayerNames.Physics2D.bullet);
        SetCollisionMask(LayerNames.Physics2D.player | LayerNames.Physics2D.floorWall);

        Connect("body_entered", this, nameof(onCollision));

        SetGravityScale(10);
    }

    /// <summary>
    /// Detects, if the bullet collide with something
    /// </summary>
    /// <param name="body"> The node, wich collides </param>
    /// <returns> If the bullet collides with a player, the name of that player else an empty string</returns>
    private string onCollision(Node body)
    {
        //creates the collision animation;
        collisionAnimation();

        //delte bullet
        GetParent().RemoveChild(this);

        //Return the players name, if the bullet collide with a player.
        if (false)
        {

        }

        return "";
    }


    /// <summary>
    /// The Animation wich should be shown, if a bullet collide.
    /// </summary>
    public abstract void collisionAnimation();

    /// <summary>
    /// To defines the direction, where the bullet is fired to
    /// </summary>
    /// <param name="direction"></param>
    public abstract void setShootDirection(float direction);

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
