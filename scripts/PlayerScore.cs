using Godot;
using System;

public partial class PlayerScore : Label
{
	public override void _Process(double delta)
	{
		GameManager gameManager = GetNode<GameManager>("%GameManager");
		this.Text = gameManager.PlayerScore.ToString();
	}
}
