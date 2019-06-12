using Godot;
using System;

/// <summary>
/// Script of StartMenuScene Root.
/// Starts Menu Music, if not already running
/// </summary>
public class StartMenuScene : Control
{
    public override void _Ready()
    {
        // Turn on the Menu Music if not already running
        var global = GetNode("/root/Global");
        var menuAudio = (AudioStreamPlayer)global.GetNode("MenuAudioStreamPlayer");
        if (!menuAudio.IsPlaying())
        {
            menuAudio.Play();
        }
    }
}
