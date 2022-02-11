using Godot;
using System;

public class SimpleTextArea : Control
{
	public string path;
	public PackedScene packedScene;

	public int index = 1;
	private bool isMouseInside = false;
	private bool isMouseDragBPressed = false;

	private Vector2 mouseDrag;
	private Vector2 thisInitialPosition;

	public SimpleTextArea()
	{
		path = "res://Assets/GDD_Maker/UI/SimpleTextArea.tscn";
		packedScene = GD.Load<PackedScene>(path);

	}

    private void FollowMouse() {
		Vector2 newposition = new Vector2(
		(GetViewport().GetMousePosition().x - mouseDrag.x + thisInitialPosition.x),
		(GetViewport().GetMousePosition().y - mouseDrag.y + thisInitialPosition.y)
		);
		this.SetPosition(newposition);

	}

    public override void _Process(float delta)
	{
		if(isMouseInside && isMouseDragBPressed){
			FollowMouse();
		}
	}

    [Obsolete]
    public override void _Ready()
	{
		//posição padrão
		this.SetPosition(new Vector2(OS.GetScreenSize().x / 3 - this.GetSize().x,
									80));

		

		TextEdit actionArea = this.GetNode<TextEdit>("TextEdit");
		actionArea.Connect("gui_input", this, "InputProcess");
		actionArea.Connect("mouse_entered", this, "mouse_entered");
		actionArea.Connect("mouse_exited", this, "mouse_exited");
	}

    [Obsolete]
    private void InputProcess(object @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.IsPressed() && eventMouseButton.ButtonIndex == ((int)ButtonList.Middle))
			{
				isMouseDragBPressed = true;
				mouseDrag = GetViewport().GetMousePosition();
				thisInitialPosition = this.GetPosition();
			}
			else {
				isMouseDragBPressed = false;
			}
		}
	}

	private void mouse_entered() {
		isMouseInside = true;
	}
	private void mouse_exited() {
		isMouseInside = false;
	}

}
