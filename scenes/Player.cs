using Godot;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

public enum Direction
{
	North,
	South,
	East,
	West
};


public partial class Player : CharacterBody2D
{
	[Export]
	public float speed = 3000.0f;

	private AnimatedSprite2D _animatedSprite;
	private Direction _direction = Direction.South;
	private bool _block_animations = false;
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("animation");
	}

	public override void _PhysicsProcess(double delta)
	{
		var inputDir = Vector2.Zero;
		var isStrike = false;

		if (Input.IsActionPressed("right")) // TODO make better with axis
		{
			inputDir.X = 1;
			_direction = Direction.East;
		}
		else if (Input.IsActionPressed("left"))
		{
			inputDir.X = -1;
			_direction = Direction.West;
		}
		if (Input.IsActionPressed("up"))
		{
			inputDir.Y = -1;
			_direction = Direction.North;
		}
		else if (Input.IsActionPressed("down"))
		{
			inputDir.Y = 1;
			_direction = Direction.South;
		}

		if (Input.IsActionPressed("grabItem"))
		{
			// find location of mouse and any rigid bodies under it
			// TODO spawn "laser ish thing" between connecting body and player
			// TODO what if if the body is too heavy it moves the player?!? could be neat
			// effect force under mouse click
		}

		if (Input.IsActionJustPressed("strike"))
		{
			isStrike = true;
		}
		animateSprite(inputDir, isStrike);
		Velocity = inputDir.Normalized() * speed * (float)delta;
		MoveAndSlide();
	}

	public void _on_Animation_Finished()
	{
		_block_animations = false;
	}

	private void animateSprite(Vector2 dir, bool isStrike)
	{
		if (_block_animations)
		{
			return;
		}
		var action = "idle";
		if (isStrike)
		{
			action = "strike";
			_block_animations = true;
		}
		else if (dir.Length() > 0)
		{
			action = "walk";
		}

		if (_direction == Direction.West)
		{
			_animatedSprite.FlipH = true;
			_animatedSprite.Play($"{action}_East");
		}
		else
		{
			_animatedSprite.FlipH = false;
			_animatedSprite.Play($"{action}_{_direction}");
		}
	}
}
