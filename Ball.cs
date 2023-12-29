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


    public override void _Ready()
    {
        LaunchBall(1);
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
	}

    //dir
    //-1 - left
    // 1 - right
    public void LaunchBall(int dir) {
        velocity = new Vector2(dir,0);
        Random rand = new Random();
        velocity = velocity.Rotated((float)rand.NextDouble()-0.5f);
        velocity *= speed;
    }
}