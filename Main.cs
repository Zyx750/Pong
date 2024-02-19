using System.Diagnostics.Contracts;
using Godot;

public partial class Main : Node
{
	Game game;
	bool paused = false;
	public void OnStartGame(bool vsCpu) {
		game = ResourceLoader.Load<PackedScene>("res://game.tscn").Instantiate() as Game;
		AddChild(game);
		GetNode<Menu>("MainMenu").QueueFree();
		game.StartGame(vsCpu);
	}

	public override void _UnhandledInput(InputEvent @event) {
		if(@event.IsActionPressed("pause")) {
			if(GetTree().Paused) {
				GetTree().Paused = false;
			}
			else {
				GetTree().Paused = true;
			}
			GetNode<Timer>("PauseTimer").Start();
		}
	}
}