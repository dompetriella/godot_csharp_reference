using Godot;

public partial class game_over_screen : Control
{
	Label _scoreLabel;

	public override void _Ready()
	{
		_scoreLabel = (Label)GetNode("/root/Game/UI/GameOverScreen/Panel/Score");
	}

	public override void _Process(double delta)
	{
	}

	public void SetScore(int newScore)
	{
		_scoreLabel.Text = "Score: " + newScore;
	}

	public void _on_retry_button_pressed()
	{
		GD.Print("game.cs>game_over_screen>_on_retry_button_pressed: Game Over!");
		GetTree().ReloadCurrentScene();
	}
}
