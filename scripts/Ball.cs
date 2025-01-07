using Godot;
using System;

public partial class Ball : RigidBody2D
{
	[Export] public float DefaultSpeed = 300.0f;
	[Export] public float Acceleration = 50.0f;

	public Vector2 direction = Vector2.Left.Normalized(); //Left as debug - TODO
	private float BallSpeed;

	public override void _Ready()
	{
		BallSpeed = DefaultSpeed;
	}

	public override void _Process(double delta)
	{
		BallSpeed += (float)delta * 2;

		Position += direction * BallSpeed * (float)delta;
	}

	public void _ResetBall()
	{
		direction = Vector2.Zero;
		Position = Vector2.Zero;
		BallSpeed = DefaultSpeed;
	}
}
