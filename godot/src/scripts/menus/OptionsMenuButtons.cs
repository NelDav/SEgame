using Godot;

// Options Menu GUI
// This class contains the user interface for the Options Menu.
// It holds buttons to navigate between Menus

public class OptionsMenuButtons : VBoxContainer
{
    Node global;
    AudioStreamPlayer menuAudio;

    public override void _Ready()
    {
        global = GetNode("/root/Global");
        menuAudio = (AudioStreamPlayer)global.GetNode("MenuAudioStreamPlayer");

        if(!menuAudio.IsPlaying())
        {
            CheckButton toggleButton = (CheckButton)GetNode("/root/OptionsMenuScene/OptionsMenuButtons/menuMusic");
            GD.Print(toggleButton + toggleButton.Name);
            toggleButton.Pressed = false;
        }
    }

    private void _on_toStartMenu_pressed()
    {
        GD.Print("Back to da roots ma friend; aka to StartMenu");
        GetTree().ChangeScene("res://src/scenes/menus/StartMenuScene.tscn");
    }

    private void _on_menueMusic_toggled(bool button_pressed)
    {
        if(button_pressed)
        {
            // Turn off the Menu Music
            menuAudio.Play();
        }
        else
        {
            // Turn off the Menu Music
            menuAudio.Stop();
        }

        GD.Print("Toggled menue music");
    }
}