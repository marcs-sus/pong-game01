using Godot;
using System;

public partial class Player2 : CharacterBody2D
{
	[Export] public float Player2Speed = 500.0f;

	public override void _Ready()
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsActionPressed("up_p2"))
		{
			velocity.Y = -Player2Speed;
		}
		else if (Input.IsActionPressed("down_p2"))
		{
			velocity.Y = Player2Speed;
		}
		else
		{
			velocity.Y = 0;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
