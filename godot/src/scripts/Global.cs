using Godot;
using System;

public class Global : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Node global = GetNode("/root/Global");
        AudioStreamPlayer menuAudio = (AudioStreamPlayer)global.GetNode("MenuAudioStreamPlayer");
        menuAudio.Play();

        GD.Print("Load Startmenu");
        GetTree().ChangeScene("res://src/scenes/menus/StartMenuScene.tscn");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
