using Godot;

/// <summary>
/// The class of bullets that belong to the standardt weapon
/// </summary>
public class StandardtBullet : Bullet
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set bullet type specific values
        SetLinearVelocity(new Vector2(1000, 50));

        //call _Ready of the base class, to init non bullet type specific values
        base._Ready();
    }

    //The animation, which is played if the bullet collides
    public override void collisionAnimation()
    {
        GD.Print("collide!");
    }
}