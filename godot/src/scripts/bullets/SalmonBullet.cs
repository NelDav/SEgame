using Godot;

/// <summary>
/// The class of bullets that belong to the standard weapon
/// </summary>
public class SalmonBullet : Bullet
{
    [Export] private float absVelocity = 1000;
    public override int Damage { get{ return 7;} }

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        //Set bullet type specific values
        //SetLinearVelocity(new Vector2(1000, 50));

        //call _Ready of the base class, to init non bullet type specific values
        base._Ready();
    }

    /// <summary>
    /// The animation, which is played if the bullet collides
    /// </summary>
    public override void collisionAnimation()
    {
        GD.Print("collide!");
    }

    public override void setShootDirection(float direction)
    {
        SetLinearVelocity(new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * absVelocity);
    }
}