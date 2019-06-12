using Godot;

/// <summary>
/// SalmonWeapon class, implementing functios of ranged weapon and weapon
/// </summary>
public class SalmonWeapon: RangedWeapon
{
    [Signal] delegate void shoot(PackedScene bullet, Vector2 location, float direction);
    private PackedScene bulletscene = GD.Load("res://src/scenes/bullets/StandardBullet.tscn") as PackedScene;

    Sprite sprite;

    public override void _Ready()
    {
        base._Ready();
        sprite = (Godot.Sprite)GetNode("SalmonWeaponTexture");

        maxAmmunition = 50;
        attackSpeed = 2.3;
        precision = 3;
        currentAmmunition =  maxAmmunition;
    }

    public override void shootBullet(Vector2 position, float rotation)
    {
        EmitSignal(nameof(shoot), bulletscene, position, rotation);
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
