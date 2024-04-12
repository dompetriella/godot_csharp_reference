using Godot;
using System.Linq;

public partial class level : Node2D
{
	[Export] PackedScene nextLevel = null;
	[Export] bool isFinalLevel = false;
	[Export] float levelTime = 5;
	start _start;
	player _player;
	exit _exit;
	Area2D _deathZone;
	Timer timerNode;
	hud _hud;
	ui_layer _uiLayer;
	float timeLeft;
	bool win = false;
	audio_player audio_player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_start = (start)GetNode("/root/Level/Start");
		_exit = (exit)GetNode("/root/Level/Exit");
		_deathZone = (Area2D)GetNode("/root/Level/Deathzone");
		_hud = (hud)GetNode("/root/Level/UILayer/HUD");
		_uiLayer = (ui_layer)GetNode("/root/Level/UILayer");
		audio_player = GetNode<audio_player>("/root/AudioPlayer");
		if (nextLevel != null)
		{
			nextLevel = GD.Load<PackedScene>(nextLevel.ResourcePath + nextLevel.ResourceName);
		}

		if (_player is null)
		{
			_player = (player)GetTree().GetFirstNodeInGroup("player");

		}
		var _traps = GetTree().GetNodesInGroup("traps");

		//GD.Print("level.cd>level>_Ready(): " + _traps.ToString());

		foreach (Node2D trap in _traps.Cast<Node2D>())
		{
			//GD.Print("level.cd>level>_Ready(): connect signal - " + trap.ToString());

			trap.Connect("touched_player", Callable.From(_on_trap_touched_player));
		}
		_player.GlobalPosition = _start.getSpawnPosition();

		_exit.BodyEntered += (player) => _on_exit_body_entered(_player);  // Hard one to figure out as less people use godot in pure c#.
																		  // Required godot api and c# documentation. Also note
																		  // variables are shared between the closure and the original scope, 
																		  // changes to the variable’s value within the closure will also 
																		  // affect the original value as an side-effect.												
		_deathZone.BodyEntered += (player) => _on_deathzone_body_entered(_player);

		timeLeft = levelTime;
		_hud.SetTimeLabel(timeLeft);

		timerNode = new Timer();
		timerNode.Name = "Level Timer";
		timerNode.WaitTime = 1;
		timerNode.Connect("timeout", Callable.From(_on_level_timer_timeout));
		AddChild(timerNode);
		timerNode.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
		{
			GetTree().Quit();
		}
		else if (Input.IsActionJustPressed("reset"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

	public void _on_deathzone_body_entered(player body)
	{
		audio_player.PlaySfx("hurt");
		ResetPlayer();
	}

	public void _on_trap_touched_player()
	{
		audio_player.PlaySfx("hurt");
		ResetPlayer();
	}

	public void ResetPlayer()
	{
		timeLeft = levelTime;                           // <BUGFIX>, now level timer allways resets after dying.
		_player.Velocity = Vector2.Zero;
		_player.GlobalPosition = _start.getSpawnPosition();
	}

	public async void _on_exit_body_entered(Node2D body)  //async required for await.
	{
		//GD.Print("level.cd>level>_on_exit_body_entered(): connect signal");

		if (body is player)
		{
			if (isFinalLevel || nextLevel != null)
			{
				_exit.animate();
				_player.active = false;
				win = true;

				await ToSignal(GetTree().CreateTimer(1.5), "timeout");  //also to suppress signal callback errors in c#.
				if (isFinalLevel)
				{
					_uiLayer.ShowWinScreen(true);
				}
				else
				{
					GetTree().ChangeSceneToPacked(nextLevel);
				}

			}
		}
	}

	public void _on_level_timer_timeout()
	{
		if (win == false)
		{
			timeLeft -= 1;
			_hud.SetTimeLabel(timeLeft);

			//GD.Print("level.cd>level>_on_level_timer_timeout(): time left: " + timeLeft);

			if (timeLeft < 0)
			{
				audio_player.PlaySfx("hurt");
				ResetPlayer();
				timeLeft = levelTime;
				_hud.SetTimeLabel(timeLeft);
			}
		}
	}
}