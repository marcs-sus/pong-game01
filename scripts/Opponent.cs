using Godot;
using System;

public partial class Opponent : CharacterBody2D
{
	[Export] public float AISpeed = 500.0f;
	[Export] public float ReactionDelay = 0.2f;
	[Export] public float ErrorMargin = 20.0f;
	[Export] public float DeadZone = 100.0f;
	[Export] public float SmoothingFactor = 0.5f;

	private CharacterBody2D ball;
	private float reactionTimer;
	float targetY = 0.0f;

	public override void _Ready()
	{
		ball = GetNode<CharacterBody2D>("../Ball");

		reactionTimer = ReactionDelay;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (ball != null)
		{
			reactionTimer -= (float)delta;

			if (reactionTimer <= 0)
			{
				reactionTimer = ReactionDelay;

				Vector2 ballVelocity = ball.Velocity;
				float ballTravelTime = (Position.X - ball.Position.X) / ballVelocity.X;

				if (ballTravelTime > 0)
				{
					targetY = ball.Position.Y + ballVelocity.Y * ballTravelTime;
				}
				else
				{
					targetY = ball.Position.Y;
				}

				targetY += GD.Randf() * ErrorMargin - (ErrorMargin / 2);
			}

			float distance = targetY - Position.Y;
			if (Math.Abs(distance) > DeadZone)
			{
				float direction = Math.Sign(distance);

				Velocity = new Vector2(0, Mathf.Lerp(Velocity.Y, direction * AISpeed, SmoothingFactor));
			}
			else
			{
				Velocity = Vector2.Zero;
			}

			MoveAndSlide();
		}
	}
}
