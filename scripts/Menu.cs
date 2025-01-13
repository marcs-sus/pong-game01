using Godot;
using System;

public partial class Menu : Control
{
	private void _OnSingleplayerPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}

	private void _OnMultiplayerPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/multiplayer_game.tscn");
	}

	private void _OnOptionsPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/options_menu.tscn");
	}

	private void _OnExitPressed()
	{
		GetTree().Quit();
	}
}
