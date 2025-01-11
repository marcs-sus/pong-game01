using Godot;
using System;

public partial class OpponentScore : Label
{
	public override void _Process(double delta)
	{
		GameManager gameManager = GetNode<GameManager>("%GameManager");
		this.Text = gameManager.OpponentScore.ToString("D2");
	}
}
