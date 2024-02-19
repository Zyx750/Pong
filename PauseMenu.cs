using Godot;
using System.Collections.Generic;

public partial class PauseMenu : Control
{
	List<Label> buttons = new();
	int active = 0;
	bool cooldown = false;
	Timer timer;
	[Signal]
    public delegate void ResumeEventHandler();
	[Signal]
    public delegate void MainMenuEventHandler();

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		foreach (Node n in GetNode("Buttons").GetChildren()) {
			if(n is Label l) {
				buttons.Add(l);
			}
		}

		buttons[active].Text = '>' + buttons[active].Text;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsAction("p1_up") || @event.IsAction("p2_up")) {
			if(cooldown) return;
			cooldown = true;
			timer.Start();
			buttons[active].Text = buttons[active].Text[1..];
			active--;
			if(active < 0) active = 2;
			buttons[active].Text = '>' + buttons[active].Text;
		}
		else if (@event.IsAction("p1_down") || @event.IsAction("p2_down")) {
			if(cooldown) return;
			cooldown = true;
			timer.Start();
			buttons[active].Text = buttons[active].Text[1..];
			active++;
			active %= 3;
			buttons[active].Text = '>' + buttons[active].Text;
		}
		else if(@event.IsActionPressed("confirm")) {
			switch(active) {
				case 0:
					EmitSignal("Resume");
					break;
				case 1:
					EmitSignal("MainMenu");
					break;
				case 2:
					GetTree().Quit();
					break;
			}
		}
	}

	public void OnTimerTimeout() {
		cooldown = false;
	}
}
