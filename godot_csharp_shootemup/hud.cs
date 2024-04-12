using Godot;

public partial class hud : Control
{
	Label _scoreLabel;
	Label _livesLeftLabel;

	public override void _Ready()
	{
		_scoreLabel = (Label)GetNode("/root/Game/UI/HUD/Score");
		_livesLeftLabel = (Label)GetNode("/root/Game/UI/HUD/LivesLeft");
	}

	public override void _Process(double delta)
	{
	}

	public void SetScoreLabel(int newScore)
	{
		_scoreLabel.Text = "Score: " + newScore;
	}

	public void SetLives(int amount)
	{
		_livesLeftLabel.Text = amount.ToString();
	}
}