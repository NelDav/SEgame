using Godot;

public class Player : RigidBody2D
{
    [Export] public int maxSpeedRight = 1500;
    [Export] public int maxSpeedLeft = 1500;

    [Export] public int accelRight = 10000;
    [Export] public int accelLeft = 10000;

    [Export] public int speedJump = 2000;

    private Vector2 initPosition;
    private bool jumpNextPhysicsFrame;
    private bool respawnNextFrame;

    public override void _Ready()
    {
        initPosition = Position;
    }

    public void respawn()
    {
        respawnNextFrame = true;
        GD.Print("HELP ME RESPAWN");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (respawnNextFrame)
        {
            respawnNextFrame = false;
            Position = initPosition;
            LinearVelocity = Vector2.Zero;
        }
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        // Get the considered floor or wall
        int floorWallInt = getCollidingFloorOrWall(state);


        //*************************************************
        // Left Right (x-Direction) Movement
        //*************************************************

        // Player will be slowed more if he touches a floor or wall, smaller factor more slowing
        float xSlowFactor = (floorWallInt == -1) ? 0.92f : 0.85f;
        // Player can't accelerate as quick if he doesn't touch a floor or wall
        float xAccelFactor = (floorWallInt == -1) ? 0.4f : 1.0f;

        // Final x-Velocity is the old x-Velocity multiplied with the slowFactor and added with the velocity from the inputs
        // The inputs create an acceleration which is converted by the Player Mass and the needed time of this physicsProcess frame into a velocity
        Vector2 finalVel = new Vector2(xSlowFactor, 1) * state.GetLinearVelocity() + xAccelFactor * getInputAccelLeftRight() * state.GetInverseMass() * GetPhysicsProcessDeltaTime();

        // Clamp the final x-Velocity to the defined max Speeds. 
        // Maybe use the old x-Velocity multiplied with the slowFactor, to allow higher x-Velocities for the player from other sources
        finalVel.x = Mathf.Clamp(finalVel.x,
            Mathf.Min(-maxSpeedLeft, xSlowFactor * state.GetLinearVelocity().x),
            Mathf.Max(maxSpeedRight, xSlowFactor * state.GetLinearVelocity().x));


        //*************************************************
        // Jump Movement
        //*************************************************

        if (floorWallInt != -1 && jumpNextPhysicsFrame)
        {
            finalVel.y = 0;
            finalVel += getJumpVelocityFromFloorWallNormal(state.GetContactLocalNormal(floorWallInt));
        }
        jumpNextPhysicsFrame = false;

        state.SetLinearVelocity(finalVel);
    }

    public override void _Input(InputEvent @event)
    {
        // The jump input event shall only be triggered once by pushing the button not permanently by holding the butten
        // The event must be checked here to achieve the desired behavior
        jumpNextPhysicsFrame = @event.IsActionPressed("game_jump");

        base._Input(@event);
    }

    // Get the id of the colliding floor or wall with the most upright surface normal
    private int getCollidingFloorOrWall(Physics2DDirectBodyState state)
    {
        int floorWall = -1;
        float angleToUpVector_abs = 9000;
        for (int i = 0; i < state.GetContactCount(); ++i)
        {
            if ((Physics2DServer.BodyGetCollisionLayer(state.GetContactCollider(i)) & LayerNames.Physics2D.floorWall) != 0)
            {
                float currentAngleToUp_abs = Mathf.Abs(state.GetContactLocalNormal(i).AngleTo(new Vector2(0, -1)));
                if (currentAngleToUp_abs < angleToUpVector_abs)
                {
                    floorWall = i;
                    angleToUpVector_abs = currentAngleToUp_abs;
                }
            }
        }
        return floorWall;
    }

    // Get the input forces of Left and Right movement
    private Vector2 getInputAccelLeftRight()
    {
        Vector2 accel = Vector2.Zero;

        if (Input.IsActionPressed("game_right"))
        {
            accel.x += accelLeft;
        }
        if (Input.IsActionPressed("game_left"))
        {
            accel.x -= accelRight;
        }

        return accel;
    }

    // Get the jump velocity. The direction is the perpendicular bisector of the surface normal and the up vector
    private Vector2 getJumpVelocityFromFloorWallNormal(Vector2 normal)
    {
        Vector2 jumpDirection = normal.Normalized() + new Vector2(0, -1);
        return jumpDirection.Normalized() * speedJump;
    }
}
