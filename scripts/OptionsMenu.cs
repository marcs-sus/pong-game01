using Godot;
using System;

public partial class OptionsMenu : Control
{
	private int MasterBus = AudioServer.GetBusIndex("Master");

	private void _OnBackPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
	}

	private void _OnFullscreenToggled(bool buttonPressed)
	{
		if (buttonPressed)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		else if (!buttonPressed)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
	}

	private void _OnVolumeSliderValueChanged(int value)
	{
		AudioServer.SetBusVolumeDb(MasterBus, value);

		if (value == - 30)
		{
			AudioServer.SetBusMute(MasterBus, true);
		}
		else
		{
			AudioServer.SetBusMute(MasterBus, false);
		}
	}
}
