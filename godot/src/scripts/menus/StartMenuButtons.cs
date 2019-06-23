using Godot;

// Start Menu GUI
// This class contains the user interface for the Start Menu.
// It holds buttons to navigate between Menus as well as a button to quit the game. 

public class StartMenuButtons : CenteredVBoxContainer
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
        // open PopUp Dialog (size: 300, 100)
        myQuitDialog.PopupCentered(new Vector2(300,100)); 
    }

    private void _on_myQuitDialog_confirmed()
    {
        GD.Print("Quit!");
        GetTree().Quit();
    }
}