using Godot;


public partial class enemy_spawner : Node2D
{
	[Signal] public delegate void enemy_spawnedEventHandler(Area2D enemyInstance); 									
	[Signal] public delegate void path_enemy_spawnedEventHandler(Path2D enemyInstance);
									//save all files&run build (or manaual build) to display signals in Godot UI!

	PackedScene _enemyScene;
	PackedScene _pathEnemyScene;

	enemy_spawner _enemySpawner;
	
	public override void _Ready()
	{
		_enemyScene = GD.Load<PackedScene>("res://scenes/enemy.tscn");
		_pathEnemyScene = GD.Load<PackedScene>("res://scenes/path_enemy.tscn");

		_enemySpawner = (enemy_spawner)GetNode("/root/Game/EnemySpawner");
	}

	public override void _Process(double delta)
	{
	}

	public void _on_timer_timeout()
	{
//		GD.Print("enemy_spawner.cs>enemy_spawner>_on_timer_timeout(): ");

		spawnEnemy();
	}
	
	public void _on_path_enemy_timer_timeout()
	{
//		GD.Print("enemy_spawner.cs>enemy_spawner>_on_path_enemy_timer_timeout(): ");

		spawnPathEnemy();
	}
	
	public void spawnEnemy()
	{
		var spawnPositions = _enemySpawner.GetNode("SpawnPositions").GetChildren();
		var randomPosition = (Node2D)spawnPositions.PickRandom();
		
//		GD.Print("enemy_spawner.cs>enemy_spawner>_spawnEnemy(): " + randomPosition.GlobalPosition.ToString());	
	
		var enemyInstance = (Area2D)_enemyScene.Instantiate();
		//GetNode("/root/Game/EnemySpawner/EnemyContainer").AddChild(enemyInstance);
		EmitSignal(SignalName.enemy_spawned, enemyInstance);
		Vector2 vector = new Vector2(0, 0);
		vector = randomPosition.GlobalPosition;
		enemyInstance.GlobalPosition = vector;			
	}

	public void spawnPathEnemy()
	{
		var enemyInstance = (Path2D)_pathEnemyScene.Instantiate();
		EmitSignal(SignalName.path_enemy_spawned, enemyInstance);
	}
}