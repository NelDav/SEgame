using Godot;
using System;

public class StartMenuButtons : VBoxContainer
{
    [Export] NodePath quitDialog;
    private ConfirmationDialog myQuitDialog;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        myQuitDialog = GetNode(quitDialog) as ConfirmationDialog;
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    private void _on_StartGame_pressed()
    {
	    //Print, not print !!!!!!!!!!afanafsnvsa12121111elf
	    GD.Print("Start Game!");
        GetTree().ChangeScene("res://src/scenes/menus/StartGameMenuScene.tscn");
    }


    private void _on_Options_pressed()
    {
	    GD.Print("Options!");
        GetTree().ChangeScene("res://src/scenes/menus/OptionsMenuScene.tscn");
    }


    private void _on_Quit_pressed()
    {
        GD.Print("Quit? Popup");
        myQuitDialog.PopupCentered(new Vector2(300,150));
    }


    private void _on_myQuitDialog_confirmed()
    {
        GD.Print("Quit!");
        GetTree().Quit();
    }

}