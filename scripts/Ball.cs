using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export] public float DefaultSpeed = 20.0f;
	[Export] public float Acceleration = 1.0f;
	[Export] public float MaxSpeed = 150.0f;
	[Export] public int RandomAngle = 5;
	[Export] public float BounceAngle = 8.5f;

	private float BallSpeed;
	private AudioStreamPlayer2D ballHitPaddle, ballHitWall, goalScored;
	Random random = new Random();

	public override void _Ready()
	{
		ballHitPaddle = GetNode<AudioStreamPlayer2D>("BallHitPaddle");
		ballHitWall = GetNode<AudioStreamPlayer2D>("BallHitWall");
		goalScored = GetNode<AudioStreamPlayer2D>("GoalScored");

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

	private void _OnBallEnteredLeftZone(Node2D body)
	{
		if (body is Ball)
		{
			_ResetBall(true);
			GameManager gameManager = GetNode<GameManager>("%GameManager");
			gameManager._IncrementScore(true);
			goalScored.Play();
			// GD.Print("Ball entered left dead zone");
		}
	}

	private void _OnBallEnteredRightZone(Node2D body)
	{
		if (body is Ball)
		{
			_ResetBall(false);
			GameManager gameManager = GetNode<GameManager>("%GameManager");
			gameManager._IncrementScore(false);
			goalScored.Play();
			// GD.Print("Ball entered right dead zone");
		}
	}

	private void _OnBallBouncedPaddle(Node2D body)
	{
		if (body.IsInGroup("Paddle"))
		{
			BallSpeed = Mathf.Min(BallSpeed + Acceleration, MaxSpeed);

			float paddleSize = body.GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.Y;

			float offset = (Position.Y - body.Position.Y) / (paddleSize / 2);
			offset = Mathf.Clamp(offset, -1.0f, 1.0f);

			Velocity = new Vector2(-Velocity.X, Velocity.Y + offset * BounceAngle).Normalized() * BallSpeed;

			ballHitPaddle.Play();
		}
		else
		{
			ballHitWall.Play();
		}
	}
}
