using Godot;
using Godot.NativeInterop;

public partial class ScoreBoard : MarginContainer
{
	int p1Score = 0;
	int p2Score = 0;
	Label p1Label;
	Label p2Label;

    public override void _Ready()
    {
        p1Label = GetNode<Label>("Player1");
		p2Label = GetNode<Label>("Player2");
    }

	public void OnGoal(int side) {
		if(side > 0) {
			p1Score++;
			p1Label.Text = p1Score.ToString();
		}
		else {
			p2Score++;
			p2Label.Text = p2Score.ToString();
		}
	}
}
