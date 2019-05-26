using Godot;

/// <summary>
/// This class is used to fetch signals from the game (player stats for example). 
/// The received updates / values are used to show in game as an overlay.
/// </summary>

public class OverlayScene : MarginContainer
{
    private Player player;

    private Label healthText;

    [Export]
    public bool showAimLines;
    [Export]
    public bool showAimCrosshair;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //connecting to the health signal changes of the player
        player = (Player)GetParent().GetParent().GetNode("Player");
        player.Connect("HealthChangeSignal", this, nameof(onHealthChange));
 
        //setting up the health value
        healthText = (Label) GetNode("Columns/HealthContainer/Background/Column/Number");
        healthText.SetText("100");
    }

    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        if (showAimCrosshair || showAimLines)
        {
            AimVisualisation();
        }
    }

    private void AimVisualisation()
    {
        //Should later be imported from weapon object
        float weaponRange = 500;
        float weaponPrecision = 10;
        float muzzleLengthOffset = 80;

        muzzleLengthOffset *= GetViewport().GetCanvasTransform().Scale.x;
        Vector2 mousePosition = RemoveOffset(GetViewport().GetMousePosition());
        Vector2 playerPosition = RemoveOffset(player.GetGlobalTransformWithCanvas().origin);
        Vector2 muzzlePosition = playerPosition + (mousePosition - playerPosition).Normalized() * muzzleLengthOffset;

        float range = weaponRange / player.GetLocalMousePosition().Length();
        Vector2 maxRangePointRelativeToPlayer = (mousePosition - playerPosition) * range;

        //Lines
        if (showAimLines)
        {
            DrawLine(muzzlePosition, muzzlePosition + maxRangePointRelativeToPlayer,
                Color.ColorN("black", alpha: 0.75f));
            DrawLine(muzzlePosition, muzzlePosition + maxRangePointRelativeToPlayer.Rotated(Mathf.Deg2Rad(weaponPrecision)),
                Color.ColorN("gray", alpha: 0.5f));
            DrawLine(muzzlePosition, muzzlePosition + maxRangePointRelativeToPlayer.Rotated(Mathf.Deg2Rad(-weaponPrecision)),
                Color.ColorN("gray", alpha: 0.5f));
        }

        //Crosshair
        if (showAimCrosshair)
        {
            Color crosshairColor = Color.ColorN("white");
            if (maxRangePointRelativeToPlayer.Length() + muzzleLengthOffset < (mousePosition - playerPosition).Length())
            {
                crosshairColor = Color.ColorN("gray");
            }
            float precision = Mathf.Sin(Mathf.Deg2Rad(weaponPrecision)) * (mousePosition - muzzlePosition).Length();
            DrawLine(mousePosition + Vector2.Up * precision, mousePosition + Vector2.Up * (precision + 7), crosshairColor);
            DrawLine(mousePosition + Vector2.Right * precision, mousePosition + Vector2.Right * (precision + 7), crosshairColor);
            DrawLine(mousePosition + Vector2.Down * precision, mousePosition + Vector2.Down * (precision + 7), crosshairColor);
            DrawLine(mousePosition + Vector2.Left * precision, mousePosition + Vector2.Left * (precision + 7), crosshairColor);
        }
    }

    public void onHealthChange(int health)
    {
        healthText.SetText(health.ToString());
    }

    /// <summary>
    /// Returns vectors usable to draw stuff relative to the viewport.
    /// <para>
    /// The position of this control's rect is set to (20,20).
    /// Because of that objects drawn in this node will be offset by that factor
    /// when using viewport coordinates to position them.
    /// This method removes that offset.
    /// </para>
    /// </summary>
    /// <param name="position">A position in the viewport's coordinate system.</param>
    /// <returns>A position in this canvas' coordinate system.</returns>
    private Vector2 RemoveOffset(Vector2 position)
    {
        return position - Vector2.One * 20;
    }
}
