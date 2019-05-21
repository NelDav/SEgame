using Godot;

public class StartMenuButtons : VBoxContainer
{
    [Export] NodePath quitDialog;
    private ConfirmationDialog myQuitDialog;

  
    public override void _Ready()
    {
        myQuitDialog = GetNode(quitDialog) as ConfirmationDialog;
        
    }

    private void _on_StartGame_pressed()
    {
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