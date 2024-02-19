using Godot;

public partial class Game : Node
{
	Ball ball;
	Timer startTimer;

	public override void _Ready() {
		GD.Print("?");
		ball = GetNode<Ball>("Ball");
		startTimer = GetNode<Timer>("StartTimer");
	}
	public void StartGame(bool vsCpu) {
		int side = (int)(GD.Randi() % 2);
		if(side == 0) side = -1;
		OnGoal(side);
	}
	public void OnGoal(int side) {
		ball.RestartBall(-side);
		startTimer.Start();
	}
}
