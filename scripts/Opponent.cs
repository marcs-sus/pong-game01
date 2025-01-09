using Godot;
using System;

public partial class Opponent : CharacterBody2D
{
	[Export] public float OpponentSpeed = 300.0f;

	public override void _Ready()
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsKeyPressed(Key.U))
			velocity.Y = -OpponentSpeed;

		Velocity = velocity;
		MoveAndSlide();
	}
}
