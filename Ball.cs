using System;
using Godot;

public partial class Ball : CharacterBody2D
{
    [Export]
    public float startSpeed = 400f;
    [Export]
    public float acceleration = 1.1f;
	private Vector2 screenSize;
    private Vector2 velocity;
    private float height;
    private int dir = 1;
    private float speed;
    private AudioStreamPlayer bounce;
    private AudioStreamPlayer goal;

    [Signal]
    public delegate void GoalEventHandler(int side); //-1 or 1


    public override void _Ready()
    {
        LaunchBall();
		screenSize = GetViewportRect().Size;
        height = (GetNode<CollisionShape2D>("CollisionShape2D").Shape as RectangleShape2D).Size.Y;
        bounce = GetNode<AudioStreamPlayer>("Bounce");
        goal = GetNode<AudioStreamPlayer>("Goal");
    }

    public override void _PhysicsProcess(double delta)
	{
        var colInfo = MoveAndCollide(velocity * (float)delta);
        //Position += velocity * (float)delta;
        if(Position.Y <= height/2) {
            velocity = velocity.Bounce(Vector2.Up);
            bounce.Play();
        }
        if(Position.Y >= screenSize.Y-height/2) {
            velocity = velocity.Bounce(Vector2.Down);
            bounce.Play();
        }

        if (colInfo != null) {
            Vector2 dir = new Vector2(colInfo.GetNormal().X*10, Position.Y - (colInfo.GetCollider() as CollisionObject2D).Position.Y).Normalized();
            speed *= acceleration;
            velocity = (dir + velocity.Bounce(colInfo.GetNormal()).Normalized()).Normalized() * speed;
            bounce.Play();
        }

        if(Position.X < -100) {
            EmitSignal("Goal", -1);
            goal.Play();
        }
        else if (Position.X > screenSize.X + 100) {
            EmitSignal("Goal", 1);
            goal.Play();
        }
	}

    public void LaunchBall() {
        velocity = new Vector2(dir,0);
        Random rand = new Random();
        velocity = velocity.Rotated((float)rand.NextDouble()*1.5f-0.75f);
        speed = startSpeed;
        velocity *= speed;
    }

    public void RestartBall(int dir) {
        this.dir = dir;
        velocity = Vector2.Zero;
        Position = screenSize/2;
    }
}
