using Godot;

public partial class start : StaticBody2D
{
	Marker2D _spawnPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_spawnPosition = (Marker2D)GetNode("SpawnPosition");
	}

	public Vector2 getSpawnPosition()
	{
		return _spawnPosition.GlobalPosition;
	}
}