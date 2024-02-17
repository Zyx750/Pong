using Godot;

public partial class Main : Node
{
	Game game;
	public void OnStartGame(bool vsCpu) {
		game = ResourceLoader.Load<PackedScene>("res://game.tscn").Instantiate() as Game;
		AddChild(game);
		GetNode<Menu>("MainMenu").QueueFree();
		game.StartGame(vsCpu);
	}
}
