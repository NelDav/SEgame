using Godot;
using System;

/// <summary>
/// A Camera that can switch between a player-following and an overview perspective.
///
/// For the player-following perspective a Camera2D Node is used.
/// For the overview perspective the extents of the current map have to be defined
/// using the Transform of an empty Node called "Extents".
/// </summary>
public class PlayerCamera : Godot.Camera2D
{
    private CameraPerspective currentPerspective;

    private const float TRANSITION_TIME = 1;
    private float transitionElapsed;
    private CameraPerspective transitionTargetPerspective;
    private Node2D transitionTargetTransform;

    public CameraPerspective CurrentPerspective
    {
        get => currentPerspective;
    }

    public override void _Ready()
    {
        GetTree().GetRoot().Connect("size_changed", this, "RefreshPerspective");

        this.ClearCurrent();
        currentPerspective = CameraPerspective.OVERVIEW;
        TransitionToPerspective(CameraPerspective.FOLLOW_PLAYER);
    }

    public override void _Process(float delta)
    {
        if (currentPerspective == CameraPerspective.TRANSITION)
        {
            transitionElapsed += delta;

            Vector2 screenSize = GetViewport().Size;

            Transform2D targetTransform = transitionTargetTransform.GlobalTransform;

            float screenAspectRatio = screenSize.Aspect();
            if (screenAspectRatio > targetTransform.Scale.Aspect())
            {
                targetTransform.Scale = new Vector2(targetTransform.Scale.y * screenAspectRatio, targetTransform.Scale.y);
            }
            else
            {
                targetTransform.Scale = new Vector2(targetTransform.Scale.x, targetTransform.Scale.x * (1 / screenAspectRatio));
            }
            targetTransform.Scale = screenSize / targetTransform.Scale;

            targetTransform.origin *= Vector2.NegOne;
            targetTransform.origin *= targetTransform.Scale;
            targetTransform.origin += screenSize * 0.5f;

            GetViewport().SetCanvasTransform(GetViewport().GetCanvasTransform().InterpolateWith(targetTransform, transitionElapsed / TRANSITION_TIME));
            
            if (transitionElapsed >= TRANSITION_TIME)
            {
                FinishTransition(transitionTargetPerspective);
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("game_switch_camera"))
        {
            if (currentPerspective == CameraPerspective.FOLLOW_PLAYER)
            {
                TransitionToPerspective(CameraPerspective.OVERVIEW);
            }
            else if (currentPerspective == CameraPerspective.OVERVIEW)
            {
                TransitionToPerspective(CameraPerspective.FOLLOW_PLAYER);
            }
        }

        base._Input(@event);
    }

    public void RefreshPerspective()
    {
        if (currentPerspective != CameraPerspective.TRANSITION)
        {
            TransitionToPerspective(currentPerspective);
        }
    }

    public void TransitionToPerspective(CameraPerspective targetPerspective)
    {
        if (currentPerspective == CameraPerspective.FOLLOW_PLAYER)
        {
            this.ClearCurrent();
        }

        transitionElapsed = 0;
        transitionTargetPerspective = targetPerspective;

        switch (targetPerspective)
        {
            case CameraPerspective.OVERVIEW:
                transitionTargetTransform = (Node2D)GetTree().GetRoot().GetNode("root/MapScene/Extents");
                break;
            case CameraPerspective.FOLLOW_PLAYER:
                transitionTargetTransform = (Node2D)this;
                break;
        }

        currentPerspective = CameraPerspective.TRANSITION;
    }

    private void FinishTransition(CameraPerspective targetPerspective)
    {
        switch (targetPerspective)
        {
            case CameraPerspective.FOLLOW_PLAYER:
                this.Align();
                this.Zoom = new Vector2(1 / GetViewport().GetCanvasTransform().Scale.x, 1 / GetViewport().GetCanvasTransform().Scale.y);
                this.MakeCurrent();
                break;
        }

        currentPerspective = targetPerspective;
    }
}
