using Godot;

public partial class audio_player : Node
{
	AudioStream _hurt;
	AudioStream _jump;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hurt = GD.Load<AudioStream>("res://assets/audio/hurt.wav");
		_jump = GD.Load<AudioStream>("res://assets/audio/jump.wav");
	}

	public async void PlaySfx(string sfx_name)
	{
		//GD.Print("audio_player_cs>audio_player>play_sfx(): ");

		AudioStreamPlayer asp = new AudioStreamPlayer();

		if (sfx_name == "hurt")
		{
			asp.Stream = _hurt;
		}
		else if (sfx_name == "jump")
		{
			asp.Stream = _jump;
		}
		else
		{
			// GD.Print("audio_player_cs>audio_player>play_sfx(): Invalid sfx name");

			return;
		}

		asp.Name = "SFX";
		AddChild(asp);
		asp.Play();

		await ToSignal(asp, "finished");
		asp.QueueFree();
	}
}