using Godot;
using System;

public class OverlayScene : MarginContainer
{

    private Label healthText;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //connecting to the health signal changes of the player
        GetParent().GetParent().GetNode("Player").Connect("HealthChangeSignal", this, nameof(_on_Health_change));
 
        //setting up the health value
        healthText = (Label) GetNode("Columns/HealthContainer/Background/Column/Number");
        healthText.SetText("100");
    }

    public void _on_Health_change(int health)
    {
       
        healthText.SetText(health.ToString());
    }

}
