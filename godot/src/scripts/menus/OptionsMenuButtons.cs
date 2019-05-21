using Godot;

public class OptionsMenuButtons : VBoxContainer
{
    public override void _Ready()
    {
        
    }

	private void _on_toStartMenu_pressed()
	{
		GD.Print("Back to da roots ma friend; aka to StartMenu");
	    GetTree().ChangeScene("res://src/scenes/menus/StartMenuScene.tscn");
	}
}