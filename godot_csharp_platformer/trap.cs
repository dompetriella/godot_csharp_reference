using Godot;

public partial class trap : Node2D
{
	[Signal] public delegate void touched_playerEventHandler();

	public void _on_area_2d_body_entered(Node2D body)
	{
		//GD.Print("jump_pad.cd>trap>_on_area_2d_body_enteredusing(): ");

		if(body.GetType() == typeof(player))
		{
			var playerBody = (player)body;	//Needed in C# as player has the jump method/function.
			EmitSignal(SignalName.touched_player);
		}
	}
}