using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SteelBeam : RigidBody2D
{
    private bool _shouldMoveTowardsMouse = false;
    private Vector2 _prevForcePosition = Vector2.Zero;

    public override void _Ready() {
        _prevForcePosition = GlobalPosition;
    }
    
    public void _on_InputEvent(Node viewPort, InputEvent @event, int shape_idx)
    {
        if (@event.IsActionPressed("grabItem"))
        {
            GD.Print("Mouse clicked");
            _shouldMoveTowardsMouse = true;
        }
        if (@event.IsActionReleased("grabItem"))
        {
            GD.Print("Mouse Released");
            _shouldMoveTowardsMouse = false;

        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (!_shouldMoveTowardsMouse)
        {
            return;
        }
        var mouseGlobalPosition = GetGlobalMousePosition();
        var forceDirection = mouseGlobalPosition - GlobalPosition;
        ApplyImpulse(forceDirection.Normalized() * 10 *(float)delta);
    }

}
