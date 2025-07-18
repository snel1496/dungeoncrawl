using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SteelBeam : RigidBody2D
{
    private bool shouldMoveTowardsMouse = false;
    public void _on_InputEvent(Node viewPort, InputEvent @event, int shape_idx)
    {
        if (@event.IsActionPressed("grabItem"))
        {
            GD.Print("Mouse clicked");
            shouldMoveTowardsMouse = true;
        }
        if (@event.IsActionReleased("grabItem"))
        {
            GD.Print("Mouse Released");
            shouldMoveTowardsMouse = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (!shouldMoveTowardsMouse)
        {
            return;
        }

        GlobalPosition = GetGlobalMousePosition();
    }

}
