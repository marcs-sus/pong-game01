using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export] public float DefaultSpeed = 20.0f;
	[Export] public float Acceleration = 1.0f;
	[Export] public float MaxSpeed = 150.0f;
	[Export] public int RandomAngle = 5;
	private float BallSpeed;

	Random random = new Random();

	public override void _Ready()
	{
		BallSpeed = DefaultSpeed;
		Position = Vector2.Zero;
		Velocity = Vector2.Zero;

		int initialAngle = random.Next(-RandomAngle, RandomAngle);
		Velocity = (random.Next(0, 2) == 0)
			? new Vector2(BallSpeed, initialAngle).Normalized() * BallSpeed
			: new Vector2(-BallSpeed, initialAngle).Normalized() * BallSpeed;
	}

	public override void _PhysicsProcess(double delta)
	{
		KinematicCollision2D collision = MoveAndCollide(Velocity);

		if (collision != null)
		{
			var normal = collision.GetNormal();
			Velocity = Velocity.Bounce(normal);
		}

		Velocity = Velocity.Normalized() * BallSpeed;
	}

	public void _ResetBall(bool side)
	{
		Position = Vector2.Zero;
		BallSpeed = DefaultSpeed;

		int angle = random.Next(-RandomAngle, RandomAngle);

		if (side)
			Velocity = new Vector2(BallSpeed, angle).Normalized() * BallSpeed;
		else if (!side)
			Velocity = new Vector2(-BallSpeed, angle).Normalized() * BallSpeed;
	}

	private void _OnBallEnteredLeftZone(Node body)
	{
		if (body is Ball)
		{
			_ResetBall(true);
			GameManager gameManager = GetNode<GameManager>("%GameManager");
			gameManager._IncrementScore(true);
			// GD.Print("Ball entered left dead zone");
		}
	}

	private void _OnBallEnteredRightZone(Node body)
	{
		if (body is Ball)
		{
			_ResetBall(false);
			GameManager gameManager = GetNode<GameManager>("%GameManager");
			gameManager._IncrementScore(false);
			// GD.Print("Ball entered right dead zone");
		}
	}

	private void _OnBallBouncedPaddle(Node body)
	{
		if (body.IsInGroup("Paddle"))
		{
			BallSpeed = Mathf.Min(BallSpeed + Acceleration, MaxSpeed);
			// GD.Print($"Ball bounced paddle. Speed: {BallSpeed}");
		}
	}
}
