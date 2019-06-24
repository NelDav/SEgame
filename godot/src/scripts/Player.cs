using Godot;
/// <summary>
///  This class discripe the player node.
///
/// It handels the physics of the figure and implement the
/// movemet operations to control the player by keyboard.
/// This class includes also the health points of the figure.
///
/// @author MichaelR, MariusS, TimoB
/// </summary>

public class Player : RigidBody2D
{
    [Export] public int maxSpeedRight = 1500;
    [Export] public int maxSpeedLeft = 1500;

    [Export] public int forceRight = 10000;
    [Export] public int forceLeft = 10000;

    [Export] public int gravityAccel = 5000;

    [Export] public int speedJump = 2000;
    [Export] public int physicFramesForJump = 4;

    private Vector2 initPosition;
    private int physicFramesSinceJumpEvent;
    private bool wantToJump;
    Sprite spriteBelow;
    Sprite spriteAbove;

    //signals to notify nodes about value changes
    //example: health value changes for OverlayScene
    [Signal]
    public delegate void HealthChangeSignal(int health);

    // Non physical data

    //health Values: 0-100. Infinity health: double.PositiveInfinety.
    private double health; //this is just a storage variable, use Health instead
    public double Health
    {
        get => health;
        set
        {
            if(value < 0)
            {
                health = 0;
            }
            else if(value > 100)
            {
                health = 100;
            }
            else
            {
                health = value;
            }

            //sends an signal about
            EmitSignal(nameof(HealthChangeSignal), Health);
        }
    }

    public Weapon CurrentWeapon
    {
        get
        {
            return GetNode<Weapon>("SalmonWeapon");
        }
    }

    public override void _Ready()
    {
        initPosition = Position;
        //initializing health value
        Health = 100;

        // initialize sprites, to flip later
        spriteBelow = (Sprite)GetNode("BelowWeaponSprite");
        spriteAbove = (Sprite)GetNode("AboveWeaponSprite");
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        // Get the considered floor or wall
        int floorWallInt = getCollidingFloorOrWall(state);


        //*************************************************
        // Left Right (x-Direction) Movement
        //*************************************************
        Vector2 finalVel = addLeftRightMovementToStateVelocity(state, floorWallInt);

        //*************************************************
        // Gravity
        //*************************************************
        finalVel = addGravityToVelocity(state, finalVel, floorWallInt);

        //*************************************************
        // Jump Movement
        //*************************************************
        finalVel = addJumpEventToVelocity(state, finalVel, floorWallInt);

        state.SetLinearVelocity(finalVel);
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        float angleRad = GetGlobalMousePosition().AngleToPoint(GetGlobalPosition());

        // get angle of mouse to player, to flip sprites horizontally
        if (angleRad > -1.5 && angleRad < 1.5)
        {
            flipSpritesHorizontally(false);
        }
        else
        {
            flipSpritesHorizontally(true);
        }
    }

    private Vector2 addLeftRightMovementToStateVelocity(Physics2DDirectBodyState state, int floorWallInt)
    {

        // Player will be slowed more if he touches a floor or wall, smaller factor more slowing
        float xSlowFactor = (floorWallInt == -1) ? 0.98f : 0.85f;
        // Player can't accelerate as quick if he doesn't touch a floor or wall
        float xForceFactor = (floorWallInt == -1) ? 0.2f : 1.0f;

        // Final x-Velocity is the old x-Velocity multiplied with the slowFactor and added with the velocity from the inputs
        // The inputs create an force which is converted by the Player Mass and the needed time of this physicsProcess frame into a velocity
        Vector2 finalVel = new Vector2(xSlowFactor, 1) * state.GetLinearVelocity() + xForceFactor * getInputForceLeftRight() * state.GetInverseMass() * GetPhysicsProcessDeltaTime();

        // Clamp the final x-Velocity to the defined max Speeds. 
        // Maybe use the old x-Velocity multiplied with the slowFactor, to allow higher x-Velocities for the player from other sources
        finalVel.x = Mathf.Clamp(finalVel.x,
            Mathf.Min(-maxSpeedLeft, xSlowFactor * state.GetLinearVelocity().x),
            Mathf.Max(maxSpeedRight, xSlowFactor * state.GetLinearVelocity().x));

        return finalVel;
    }

    private Vector2 addGravityToVelocity(Physics2DDirectBodyState state, Vector2 inputVelocity, int floorWallInt)
    {
        float gravityFactor = 1.0f;
        if (floorWallInt != -1)
        {
            Vector2 floorNormal = state.GetContactLocalNormal(floorWallInt).Normalized();
            gravityFactor = 1.0f - Mathf.Max(floorNormal.Dot(Vector2.Up), 0.0f);
        }

        inputVelocity.y += gravityFactor * gravityAccel * GetPhysicsProcessDeltaTime();
        return inputVelocity;

    }

    private Vector2 addJumpEventToVelocity(Physics2DDirectBodyState state, Vector2 inputVelocity, int floorWallInt)
    {
        // After a while the player doesn't want to jump anymore
        if (physicFramesSinceJumpEvent > physicFramesForJump)
        {
            wantToJump = false;
        }
        ++physicFramesSinceJumpEvent;

        // If touching a floorWall and the player still wants to jump -> (s)he'll jump
        if (floorWallInt != -1 && wantToJump)
        {
            inputVelocity.y = 0;
            inputVelocity += getJumpVelocityFromFloorWallNormal(state.GetContactLocalNormal(floorWallInt));
            wantToJump = false;
        }
        return inputVelocity;

    }

    public override void _Input(InputEvent @event)
    {
        // The jump input event shall only be triggered once by pushing the button not permanently by holding the butten
        // The event must be checked here to achieve the desired behavior
        if (@event.IsActionPressed("game_jump"))
        {
            wantToJump = true;
            physicFramesSinceJumpEvent = 0;
        }

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
                float currentAngleToUp_abs = Mathf.Abs(state.GetContactLocalNormal(i).AngleTo(Vector2.Up));
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
    private Vector2 getInputForceLeftRight()
    {
        Vector2 force = Vector2.Zero;

        if (Input.IsActionPressed("game_right"))
        {
            force.x += forceLeft;
        }
        if (Input.IsActionPressed("game_left"))
        {
            force.x -= forceRight;
        }

        return force;
    }

    // Get the jump velocity. The direction is the perpendicular bisector of the surface normal and the up vector
    private Vector2 getJumpVelocityFromFloorWallNormal(Vector2 normal)
    {
        Vector2 jumpDirection = normal.Normalized() + Vector2.Up;
        if (jumpDirection.Equals(Vector2.Zero))
        {
            return Vector2.Zero;
        }
        return jumpDirection.Normalized() * speedJump;
    }

    public void getDamage(double damage)
    {
        if (Health <= 0)
        {
            return;
        }
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            // Player is dead.
        }
    }

    private void flipSpritesHorizontally(bool flip)
    {
        if (flip)
        {
            spriteBelow.SetScale(new Vector2(-1, 1));
            spriteAbove.SetScale(new Vector2(-1, 1));
        }
        else
        {
            spriteBelow.SetScale(new Vector2(1, 1));
            spriteAbove.SetScale(new Vector2(1, 1));
        }
    }
}
