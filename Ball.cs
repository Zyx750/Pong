using System;
using Godot;

public partial class Ball : CharacterBody2D
{
    [Export]
    public float speed = 400f;
    [Export]
    public float acceleration = 1.1f;
	private Vector2 screenSize;
    private Vector2 velocity;
    private float height;
    private int dir = 1;

    [Signal]
    public delegate void GoalEventHandler(int side); //-1 or 1


    public override void _Ready()
    {
        LaunchBall();
		screenSize = GetViewportRect().Size;
        height = (GetNode<CollisionShape2D>("CollisionShape2D").Shape as RectangleShape2D).Size.Y;
    }

    public override void _PhysicsProcess(double delta)
	{
        var colInfo = MoveAndCollide(velocity * (float)delta);
        //Position += velocity * (float)delta;
        if(Position.Y <= height/2) {
            velocity = velocity.Bounce(Vector2.Up);
        }
        if(Position.Y >= screenSize.Y-height/2) {
            velocity = velocity.Bounce(Vector2.Down);
        }

        if (colInfo != null) {
            velocity = velocity.Bounce(colInfo.GetNormal());
            velocity *= acceleration;
        }

        if(Position.X < -100) EmitSignal("Goal", -1);
        else if (Position.X > screenSize.X + 100) EmitSignal("Goal", 1);
	}

    public void LaunchBall() {
        velocity = new Vector2(dir,0);
        Random rand = new Random();
        velocity = velocity.Rotated((float)rand.NextDouble()*1.5f-0.75f);
        velocity *= speed;
    }

    public void RestartBall(int dir) {
        this.dir = dir;
        velocity = Vector2.Zero;
        Position = screenSize/2;
    }
}
