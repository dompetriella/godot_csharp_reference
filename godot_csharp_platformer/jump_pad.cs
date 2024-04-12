using Godot;
using System;

public partial class jump_pad : Area2D
{
	[Export] float jumpForce = 300.0f;
	AnimatedSprite2D _annimatedSprite;
	Area2D _jumpPad;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_annimatedSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
	}

	public void _on_body_entered(Node2D body)
	{
		//GD.Print("jump_pad.cd>jum_pad>_on_body_entered(): " + body.GetType() + " " + jumpForce);

		if (body.GetType() == typeof(player))
		{
			_annimatedSprite.Play("jump");
			var playerBody = (player)body;  //Needed in C# as player has the jump method/function.
			playerBody.jump(jumpForce);
		}
	}
}