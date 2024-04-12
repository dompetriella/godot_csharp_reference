using Godot;
using System;

public partial class path_enemy : Path2D
{
	Godot.PathFollow2D _pathfollow;
	enemy _enemy;

	public override void _Ready()
	{
			_pathfollow = GetNode<PathFollow2D>("PathFollow2D");
			_enemy = GetNode<enemy>("PathFollow2D/enemy");

			_pathfollow.ProgressRatio = 1;
	}

	public override void _Process(double delta)
	{
		float deltaf = Convert.ToSingle(delta);
		_pathfollow.ProgressRatio -= .25f * deltaf;
		if (_pathfollow.ProgressRatio <= 0)
		{
			QueueFree();
		}
	}
}