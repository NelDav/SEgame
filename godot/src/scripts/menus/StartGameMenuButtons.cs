using Godot;
using System;

public class StartGameMenuButtons : VBoxContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_toStartMenu_pressed()
		{
		    GD.Print("Back to da roots ma friend; aka to StartMenu");
			GetTree().ChangeScene("res://src/scenes/menus/StartMenuScene.tscn");
		}
		
		
		private void _on_StartGame_pressed()
		{
		    GD.Print("Start Game!");
		    GetTree().ChangeScene("res://src/scenes/InGameScene.scn");
		}

}