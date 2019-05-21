using Godot;

public class StartGameMenuButtons : VBoxContainer
{
    public override void _Ready()
    {
        
    }

	private void _on_toStartMenu_pressed()
	{
	    GD.Print("Back to da roots ma friend; aka to StartMenu");
		GetTree().ChangeScene("res://src/scenes/menus/StartMenuScene.tscn");
	}

	private void _on_StartGame_pressed()
	{
	    GD.Print("Start Game!");
	    GetTree().ChangeScene("res://src/scenes/InGameScene.tscn");
	}
}