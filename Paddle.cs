using Godot;
public partial class Paddle : CharacterBody2D
{
	[Export]
	private float speed = 400.0f;
	[Export(PropertyHint.Range, "1,2")]
	private int player = 1;
	[Export]
	public bool ai = false;
	private Vector2 screenSize;
	private float height;
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
		height = (GetNode<CollisionShape2D>("CollisionShape2D").Shape as RectangleShape2D).Size.Y;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!ai) {
			float input = Input.GetAxis($"p{player}_up", $"p{player}_down");
			Vector2 velocity = new Vector2(0, input * speed);
			Position += velocity * (float)delta;
		}
		Position = new Vector2(Position.X, Mathf.Clamp(Position.Y, height/2, screenSize.Y-height/2));
	}
}
