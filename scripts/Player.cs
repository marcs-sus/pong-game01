using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float PlayerSpeed = 300.0f;

	public override void _Ready()
	{

	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsActionPressed("up"))
		{
			velocity.Y = -PlayerSpeed;
		}
		else if (Input.IsActionPressed("down"))
		{
			velocity.Y = PlayerSpeed;
		}
		else
		{
			velocity.Y = 0;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
