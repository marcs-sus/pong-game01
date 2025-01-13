using Godot;
using System;

public partial class GameManager : Node
{
	private Node2D gameMode;

	public int PlayerScore = 0;
	public int OpponentScore = 0;

	public override void _Ready()
	{
		gameMode = (Node2D)GetParent();

		PlayerScore = 0;
		OpponentScore = 0;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
		{
			GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
		}
	}

	public void _IncrementScore(bool side)
	{
		if (side)
		{
			OpponentScore++;
			GD.Print($"Player: {PlayerScore} Opponent (added): {OpponentScore}");
		}
		else if (!side)
		{
			PlayerScore++;
			GD.Print($"Player (added): {PlayerScore} Opponent: {OpponentScore}");
		}
	}
}
