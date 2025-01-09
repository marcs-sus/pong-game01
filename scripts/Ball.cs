using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export] public float DefaultSpeed = 10.0f;
	[Export] public float Acceleration = 1.0f;
	[Export] public int RandomAngle = 5;
	private float BallSpeed;

	Random random = new Random();

	public override void _Ready()
	{
		BallSpeed = DefaultSpeed;

		if (random.Next(0, 2) == 0)
			Velocity = new Vector2(BallSpeed, 0);
		else
			Velocity = new Vector2(-BallSpeed, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		KinematicCollision2D collision = MoveAndCollide(Velocity);

		if (collision != null)
		{
			var normal = collision.GetNormal();
			Velocity = Velocity.Bounce(normal);
		}
	}

	public void _ResetBall(bool side)
	{
		Position = Vector2.Zero;
		BallSpeed = DefaultSpeed;

		if (side)
			Velocity = new Vector2(BallSpeed, random.Next(-RandomAngle, RandomAngle));
		else if (!side)
			Velocity = new Vector2(-BallSpeed, random.Next(-RandomAngle, RandomAngle));
	}

	private void _OnBallEnteredLeftZone(Node body)
	{
		_ResetBall(true);
		GameManager gameManager = GetNode<GameManager>("%GameManager");
		gameManager._IncrementScore(true);
		GD.Print("Ball entered left dead zone");
	}

	private void _OnBallEnteredRightZone(Node body)
	{
		_ResetBall(false);
		GameManager gameManager = GetNode<GameManager>("%GameManager");
		gameManager._IncrementScore(false);
		GD.Print("Ball entered right dead zone");
	}
}
