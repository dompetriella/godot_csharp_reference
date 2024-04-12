
using Godot;

public partial class game : Node2D
{
	int lives = 3;
	int score = 0;

	player _player;
	hud _hud;
	PackedScene _gameOverScreenScene;
	game_over_screen _gameOverScreen;
	AudioStreamPlayer _enemyHitSound;
	AudioStreamPlayer _playerDamageSound;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_gameOverScreenScene = GD.Load<PackedScene>("res://scenes/game_over_screen.tscn");

		_player = (player)GetNode("/root/Game/Player");
		_hud = (hud)GetNode("/root/Game/UI/HUD");
		_enemyHitSound = (AudioStreamPlayer)GetNode("/root/Game/EnemyHitSound");
		_playerDamageSound = (AudioStreamPlayer)GetNode("/root/Game/PlayerDamageSound");


		_hud.SetScoreLabel(score);
		_hud.SetLives(lives);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_deathzone_area_entered (enemy enemy)
	{
		enemy.die();
	}

	public async void _on_player_took_damage()
	{
		lives -= 1;
		_playerDamageSound.Play();
		_hud.SetLives(lives);

		if (lives == 0) 
		{
			GD.Print("game.cs>game>_game_on_took_damage(): Game Over!");
			_player.die();

			await ToSignal(GetTree().CreateTimer(1.5), "timeout");

			_gameOverScreen = (game_over_screen)_gameOverScreenScene.Instantiate();
			var ui = GetNode<CanvasLayer>("/root/Game/UI");
			ui.AddChild(_gameOverScreen);
			_gameOverScreen.SetScore(score);
		}
		else
		{
			GD.Print("game.cs>game>_game_on_took_damage(): lives: " + lives);

		}
	}

	public void _on_enemy_spawner_enemy_spawned(Area2D enemyInstance)
	{
		enemyInstance.Connect("died",Callable.From(_on_enemy_died));
		GetNode("/root/Game/EnemySpawner/EnemyContainer").AddChild(enemyInstance);
	}

	public void _on_enemy_spawner_path_enemy_spawned(Path2D pathEnemyInstance)
	{
		GetNode("/root/Game/EnemySpawner/EnemyContainer").AddChild(pathEnemyInstance);
		pathEnemyInstance.GetNode("PathFollow2D/enemy").Connect("died",Callable.From(_on_enemy_died));
	}

	public void _on_enemy_died()
	{
		score += 100;
		GD.Print("game.cs>game>_on_enemy_died: Enemy Died!, Score= " + score);
		_hud.SetScoreLabel(score);
		_enemyHitSound.Play();
	}
}