using Godot;

/// <summary>
/// This class is used to fetch signals from the game (player stats for example). 
/// The received updates / values are used to show in game as an overlay.
/// </summary>

public class OverlayScene : MarginContainer
{

    private Label healthText;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //connecting to the health signal changes of the player
        GetParent().GetParent().GetNode("Player").Connect("HealthChangeSignal", this, nameof(onHealthChange));
 
        //setting up the health value
        healthText = (Label) GetNode("Columns/HealthContainer/Background/Column/Number");
        healthText.SetText("100");
    }

    public void onHealthChange(int health)
    {
       
        healthText.SetText(health.ToString());
    }

}
