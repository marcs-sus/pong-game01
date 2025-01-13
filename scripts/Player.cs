using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float PlayerSpeed = 500.0f;

	private Node2D gameMode;
	private string actionName;

	public override void _Ready()
	{
		gameMode = (Node2D)GetParent();

		actionName = gameMode.Name == "MultiplayerGame" ? "_p1" : "";
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsActionPressed($"up{actionName}"))
		{
			velocity.Y = -PlayerSpeed;
		}
		else if (Input.IsActionPressed($"down{actionName}"))
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
