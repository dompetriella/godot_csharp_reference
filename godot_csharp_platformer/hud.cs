using Godot;

public partial class hud : Control
{
	Label _label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_label = (Label)GetNode("TimeLabel");
	}

	public void SetTimeLabel(float value)
	{
		_label.Text = "TIME: " + value.ToString();
	}
}