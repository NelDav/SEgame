using Godot;

/// <summary>
/// A VBoxContainer, which centers it selfe.
/// For that it is important, that the Signal "resized()" of the container above the VBoxContainer is binded to the "_on_Container_resized" slot.
/// </summary>

public class CenteredVBoxContainer : VBoxContainer
{
    private void _on_Container_resized()
    {
        this.RectPosition = (OS.WindowSize - this.RectSize) / 2;
    }
}
