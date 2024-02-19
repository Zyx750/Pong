using Godot;

public partial class Main : Node
{
	Game game;
	bool paused = false;
	public override void _Ready() {
		GD.Randomize();
	}
	
	public void OnStartGame(bool vsCpu) {
		game = ResourceLoader.Load<PackedScene>("res://game.tscn").Instantiate() as Game;
		AddChild(game);
		GetNode<Menu>("MainMenu").QueueFree();
		game.StartGame(vsCpu);
	}


	public void OnMainMenu() {
		Menu menu = ResourceLoader.Load<PackedScene>("res://main_menu.tscn").Instantiate() as Menu;
		AddChild(menu);
		menu.StartGame += OnStartGame;
		game.QueueFree();
		game = null;
		GetNode<PauseMenu>("PauseMenu")?.QueueFree();
		GetTree().Paused = false;
	}

	public void OnUnpause() {
		GetTree().Paused = false;
		GetNode<PauseMenu>("PauseMenu")?.QueueFree();
	}

	public override void _UnhandledInput(InputEvent @event) {
		if(game == null) return;
		if(@event.IsActionPressed("pause")) {
			if(GetTree().Paused) {
				OnUnpause();
			}
			else {
				GetTree().Paused = true;
				PauseMenu pause = ResourceLoader.Load<PackedScene>("res://pause_menu.tscn").Instantiate() as PauseMenu;
				AddChild(pause);
				pause.Resume += OnUnpause;
				pause.MainMenu += OnMainMenu;
			}
		}
	}
}