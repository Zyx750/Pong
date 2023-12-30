using Godot;
using System;

public partial class Main : Node2D
{
	Ball ball;
	Timer startTimer;

	public override void _Ready() {
		ball = GetNode<Ball>("Ball");
		startTimer = GetNode<Timer>("StartTimer");

		OnGoal(-1);
	}
	public void OnGoal(int side) {
		ball.RestartBall(-side);
		startTimer.Start();
	}
}
