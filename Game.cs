using Godot;

public partial class Game : Node
{
	Ball ball;
	Timer startTimer;
	bool vsCpu;

	public override void _Ready() {
		ball = GetNode<Ball>("Ball");
		startTimer = GetNode<Timer>("StartTimer");
	}
	public void StartGame(bool vsCpu) {
		int side = (int)(GD.Randi() % 2);
		if(side == 0) side = -1;
		this.vsCpu = vsCpu;
		if(vsCpu) GetNode<Paddle>("Paddle2").ai = true;
		else GetNode<Paddle>("Paddle2").ai = false;
		OnGoal(side);
	}
	public void OnGoal(int side) {
		ball.RestartBall(-side);
		startTimer.Start();
	}
	public override void _PhysicsProcess(double delta) {
		if(vsCpu) {
			Paddle paddle = GetNode<Paddle>("Paddle2");
			Ball ball = GetNode<Ball>("Ball");
			if(ball.velocity.X > 0) {
				Vector2 velocity;
				if(ball.Position.Y < paddle.Position.Y) {
					velocity = new Vector2(0, -300f);
				}
				else {
					velocity = new Vector2(0, 300f);
				}
				paddle.Position += velocity * (float)delta;
			}
		}
	}
}
