using Godot;
using System;


abstract public class RangedWeapon : Weapon
{

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        // click to shoot bullets
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == (int)ButtonList.Left && mouseButton.Pressed)
            {
                // TODO: calculate offset from sprite image
                int offset = 80;
                Vector2 pos = GetGlobalPosition();
                Vector2 norm = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                Vector2 res = pos + norm * offset;


                shootBullet(res, Rotation);
            }
        }
    }

    public abstract void shootBullet(Vector2 position, float rotation);

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        float angleRad = GetGlobalMousePosition().AngleToPoint(GetGlobalPosition());

        // get angle of mouse to weapon, to flip sprite vertically
        if (angleRad > -1.5 && angleRad < 1.5)
            setFlip(false);
        else
            setFlip(true);

        // rotate weapon
        SetGlobalRotation(angleRad);
    }
}
