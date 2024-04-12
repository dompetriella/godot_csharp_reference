using Godot;

public partial class exit : Area2D
{
	AnimatedSprite2D _annimatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_annimatedSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
	}

	public void animate()
	{
		_annimatedSprite.Play("default");
	}
}