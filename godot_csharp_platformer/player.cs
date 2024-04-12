using Godot;

public partial class player : CharacterBody2D
{
	public const float gravity = 400.0f;
	public const float speed = 125.0f;
	public const float jumpForce = 200.0f;
	AnimatedSprite2D _annimatedSprite;
	public bool active = true;
	audio_player audio_player;

	public override void _Ready()
	{
		_annimatedSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
		audio_player = GetNode<audio_player>("/root/AudioPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;        //Needed in C# as velocity is and Vector2.

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		if (velocity.Y > 500)
		{
			velocity.Y = 500;
		}

		float direction = 0;

		if (active)
		{
			// Handle Jump.
			if (Input.IsActionJustPressed("jump") && IsOnFloor())
			{
				//GD.Print("player.cd>player>_PhysicsProcess: jump");

				jump(jumpForce);
				velocity = Velocity;
			}

			direction = Input.GetAxis("move_left", "move_right");
			if (direction != 0)
			{
				_annimatedSprite.FlipH = direction == -1;
			}
		}

		velocity.X = direction * speed;
		Velocity = velocity;
		MoveAndSlide();
		UpdateAnimantions(direction);
	}

	public void jump(float force)
	{
		Vector2 velocity = Velocity;        //Needed in C# as velocity is and Vector2.
		velocity.Y = -force;
		Velocity = velocity;
		audio_player.PlaySfx("jump");
	}

	public void UpdateAnimantions(float direction)
	{
		if (IsOnFloor())
		{
			if (direction == 0)
			{
				_annimatedSprite.Play("idle");
			}
			else
			{
				_annimatedSprite.Play("run");
			}
		}
		else
		{
			if (Velocity.Y < 0)
			{
				_annimatedSprite.Play("fall");
			}
			else
			{
				_annimatedSprite.Play("jump");
			}
		}
	}
}