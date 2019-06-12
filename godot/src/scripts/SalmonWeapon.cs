using Godot;

/// <summary>
/// SalmonWeapon class, implementing functios of ranged weapon and weapon
/// </summary>
public class SalmonWeapon: RangedWeapon
{
    [Signal] delegate void shoot(PackedScene bullet, Vector2 location, float direction);

    public override double AttackSpeed { get{ return 2.3; } }
    public override int Precision { get{ return 3; } }
    public override int MaxAmmunition { get{ return 50; } }


    private PackedScene bulletscene = GD.Load("res://src/scenes/bullets/StandardBullet.tscn") as PackedScene;

    Sprite sprite;

    public override void _Ready()
    {
        base._Ready();
        sprite = (Godot.Sprite)GetNode("SalmonWeaponTexture");

        CurrentAmmunition = MaxAmmunition;
    }

    public override void shootBullet(Vector2 position, float rotation)
    {   
        if(CurrentAmmunition > 0)
        {
            EmitSignal(nameof(shoot), bulletscene, position, rotation);
            CurrentAmmunition--;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    }

    public override void setFlip(bool flip)
    {
        if (sprite == null)
        {
            return;
        }
        sprite.SetFlipV(flip);
    }
}
