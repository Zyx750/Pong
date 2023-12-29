using Godot;
public partial class Paddle : Area2D
{
	[Export]
	private float speed = 300.0f;
	[Export(PropertyHint.Range, "1,2")]
	private int player = 1;
	private Vector2 screenSize;
	private float height;
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
		height = (GetNode<CollisionShape2D>("CollisionShape2D").Shape as RectangleShape2D).Size.Y;
	}

	public override void _PhysicsProcess(double delta)
	{
		float input = Input.GetAxis($"p{player}_up", $"p{player}_down");
		Vector2 velocity = new Vector2(0, input * speed);
		Position += velocity * (float)delta;
		Position = new Vector2(Position.X, Mathf.Clamp(Position.Y, height/2, screenSize.Y-height/2));
	}
}
