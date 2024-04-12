using Godot;

public partial class ui_layer : CanvasLayer
{
	Control _winScreen;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_winScreen = GetNode<Control>("WinScreen");
	}

	public void ShowWinScreen(bool flag)
	{
		_winScreen.Visible = flag;
	}
}