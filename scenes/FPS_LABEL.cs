using Godot;
using System;

public partial class FPS_LABEL : Label
{
    public override void _Process(double delta)
    {
        Text = Engine.GetFramesPerSecond().ToString();
    }
}
