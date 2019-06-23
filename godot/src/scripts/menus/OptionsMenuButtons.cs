using Godot;

// Options Menu GUI
// This class contains the user interface for the Options Menu.
// It holds buttons to navigate between Menus

public class OptionsMenuButtons : VBoxContainer
{
    private void _on_toStartMenu_pressed()
    {
        GD.Print("Back to da roots ma friend; aka to StartMenu");
        GetTree().ChangeScene("res://src/scenes/menus/StartMenuScene.tscn");
    }

    private void _on_OptionsMenuScene_resized()
    {
        this.RectPosition = (OS.WindowSize - this.RectSize) / 2;
    }
}