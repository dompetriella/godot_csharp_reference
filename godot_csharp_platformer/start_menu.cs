using Godot;

public partial class start_menu : Control
{
	public void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/level.tscn");
	}

	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
}