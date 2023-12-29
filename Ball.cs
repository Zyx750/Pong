using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    private Vector2 velocity;
    private float height;
    [Export]
    public float speed = 200f;
	private Vector2 screenSize;


    public override void _Ready()
    {
        velocity = new Vector2(speed, speed);
		screenSize = GetViewportRect().Size;
        height = (GetNode<CollisionShape2D>("CollisionShape2D").Shape as RectangleShape2D).Size.Y;
    }

    public override void _PhysicsProcess(double delta)
	{
        var coll = MoveAndCollide(velocity * (float)delta);
        //Position += velocity * (float)delta;
        if(Position.Y <= height/2) {
            velocity = velocity.Bounce(Vector2.Up);
        }
        if(Position.Y >= screenSize.Y-height/2) {
            velocity = velocity.Bounce(Vector2.Down);
        }

        if(coll != null) {
            GD.Print("TEST");
        }
	}

    
}
