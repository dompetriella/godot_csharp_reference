using Godot;
using System;

public partial class rocket : Area2D
{
	[Export] public int speed = 500;
	Vector2 _velocity = new Vector2(0, 0);

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		float deltaF = Convert.ToSingle(delta);
		
		_velocity = Vector2.Right.Rotated(Rotation) * speed * deltaF;
		Translate(_velocity);
	}

	public void _on_visible_notifier_2d_screen_exited()
	{
		QueueFree();
	}

	public void _rocket_on_area_entered(enemy area2DEntered)
	{
		var hitByEnemytype = area2DEntered.GetNodeOrNull("Enemy");
		GD.Print("rocket.cs>rocket>_on_area_entered: - enemy -" + hitByEnemytype);

		QueueFree();
		area2DEntered.shot();
	}
}