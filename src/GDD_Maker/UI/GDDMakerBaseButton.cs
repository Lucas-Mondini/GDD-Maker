using Godot;
using System;

public class GDDMakerBaseButton : Godot.Button
{
	protected string name;
	public GDDMakerBaseButton() {
		this.name = "Default";
	}
	
	public override void _Ready()
	{
		
	}
	
	protected void _on_Button_pressed()
	{
	GD.Print("Button " + name + " pressed!");
	}
}

