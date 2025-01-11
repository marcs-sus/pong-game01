using Godot;
using System;

public partial class Opponent : CharacterBody2D
{
	[Export] public float AISpeed = 500.0f;
	[Export] public float ReactionDelay = 0.5f;
	[Export] public float ErrorMargin = 20.0f;

	private CharacterBody2D ball;

	public override void _Ready()
	{
		ball = GetNode<CharacterBody2D>("../Ball");
	}

	public override void _PhysicsProcess(double delta)
	{
		
	}

	private void _MoveTowardsBall()
	{
		
	}
}
