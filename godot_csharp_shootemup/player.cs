using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Signal] public delegate void took_damageEventHandler(); //save all files&run build to display in godot!
	AudioStreamPlayer _rocketShotSound;

	[Export] public float Speed = 300.0f;
	[Export] public float SpeedFactor = 50.0f; //too allign with speed on the course and relative speed of enemies.
	player _player;
	PackedScene _rocketScene;

	public override void _Ready()
	{
		_player = (player)GetNode("/root/Game/Player");
		_rocketScene = GD.Load<PackedScene>("res://scenes/rocket.tscn");
		_rocketShotSound = (AudioStreamPlayer)GetNode("RocketShotSound");
	}

public override void _Process(double delta)
{
	if(Input.IsActionJustReleased("shoot"))
	{
		Shoot();
	}
}

public override void _PhysicsProcess(double delta)
	{
		float deltaF = Convert.ToSingle(delta);
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed * deltaF;
			velocity.Y = direction.Y * Speed * deltaF;

			var screen_size = GetViewportRect().Size;
			if (GlobalPosition.X < 32)
			{
				velocity.X = Speed * deltaF;
			}
			if (GlobalPosition.X > screen_size.X - 32)
			{
				velocity.X = -Speed * deltaF;
			}
			if (GlobalPosition.Y < 72)
			{
				velocity.Y = Speed * deltaF;
			}
			if (GlobalPosition.Y > screen_size.Y - 72 )
			{
				velocity.Y = -Speed * deltaF;
			}

		}
		else
		{
			velocity.X = Mathf.MoveToward(0, 0, Speed * deltaF);
			velocity.Y = Mathf.MoveToward(0, 0, Speed * deltaF);
		}

		Velocity = velocity * SpeedFactor;  //SpeedFactor is to relative change to the viable speed of the course.
		MoveAndSlide();

//		GD.Print("player.cs>player>_PhysicsProcess: " + GlobalPosition + " ( " + velocity + " )");		
	}

	public void Shoot()
	{
		GD.Print("player.cs>player>Shoot(): ");		

		var rocketInstance = (Area2D)_rocketScene.Instantiate();
		GetNode("/root/Game/Player/RocketContainer").AddChild(rocketInstance);
		Vector2 vector = new Vector2();
		vector.X = GlobalPosition.X + 72;
		vector.Y = GlobalPosition.Y;
		rocketInstance.GlobalPosition = vector;

		_rocketShotSound.Play();
	}

	public void TakeDamage()
	{
		//GD.Print("player.cs>player>TakeDamage: ");
		EmitSignal(SignalName.took_damage);
	}

	public void die()
	{
		QueueFree();
	}
}