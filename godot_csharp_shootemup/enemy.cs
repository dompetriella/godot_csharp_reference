using Godot;
using System;

public partial class enemy : Area2D
{
	[Signal] public delegate void diedEventHandler();
				//save all files&run build (or manaual build) to display signals in Godot UI!
	[Export] public int speed = 300;
	Vector2 _velocity = new Vector2(0, 0);
	 
	public override void _Ready()
	{
//		AddUserSignal(SignalName.died); //done by godot after save all&run build!
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		float deltaF = Convert.ToSingle(delta);

		_velocity = Vector2.Left.Rotated(Rotation) * speed * deltaF;
		Translate(_velocity);
	}

	public void die()
	{
		QueueFree();
	}

	public void shot()
	{
		GD.Print("enemy.cs>enemy>shot(): - enemy shot-");

		QueueFree();
		EmitSignal(SignalName.died);
	}

	public void _enemy_on_body_shape_entered(player body)
	{
		body.TakeDamage();
		shot();
	}
}