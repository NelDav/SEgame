using Godot;
using System;
using CustomExtensions;

public class Camera2D : Godot.Camera2D
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
        currentPerspective = CameraPerspective.FOLLOW_PLAYER;
        this.MakeCurrent();
    }

    public override void _Process(float delta)
    {
        if (currentPerspective == CameraPerspective.TRANSITION)
        {
            transitionElapsed += delta;

            Transform2D targetTransform = transitionTargetTransform.Transform;
            targetTransform.origin *= Vector2.NegOne;
            targetTransform.origin += new Vector2(GetViewport().Size.Scale(0.5f));
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
            //TODO Handle TRANSITION case
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
                transitionTargetTransform = (Node2D)this.GetParent();
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
                this.MakeCurrent();
                break;
        }

        currentPerspective = targetPerspective;
    }
}
