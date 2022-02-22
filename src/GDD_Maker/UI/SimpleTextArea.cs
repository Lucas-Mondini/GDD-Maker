using Godot;
using System;

public class SimpleTextArea : Control
{
	[Signal]
	public delegate void nameChanged();
	[Signal]
	public delegate void textAreaSelected();
	[Signal]
	public delegate void textAreaDeselected();
	public string path;
	public string name;
	public PackedScene packedScene;

	public int index = -1;
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

		

		TextEdit textEditBody = this.GetNode<TextEdit>("TextEdit");
		textEditBody.Connect("gui_input", this, "InputProcess");
		textEditBody.Connect("mouse_entered", this, "mouse_entered");
		textEditBody.Connect("mouse_exited", this, "mouse_exited");
		textEditBody.Connect("focus_entered", this, "TextAreaSelected");
		textEditBody.Connect("focus_exited", this, "TextAreaDeselected");

		TextEdit textEditTitle = this.GetNode<TextEdit>("Title");
		textEditTitle.Connect("text_changed", this, "setTitleName");
		textEditTitle.Connect("focus_entered", this, "TextAreaSelected");
		textEditTitle.Connect("focus_exited", this, "TextAreaDeselected");
		textEditTitle.SetText(name);

		SetName(name);
		GD.Print(name);
		
	}

	private void TextAreaSelected() {
		this.EmitSignal("textAreaSelected");
	}
	private void TextAreaDeselected() {
		this.EmitSignal("textAreaDeselected");
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

    [Obsolete]
    private void setTitleName() {
		TextEdit textEditTitle = this.GetNode<TextEdit>("Title");
		string text = textEditTitle.GetText();

		if (text != "")
			this.SetName(text);
		else {
			this.SetName("SimpleTextArea");
			this.GetNode<TextEdit>("Title").SetText("SimpleTextArea");
		}

		string name = this.GetName();
		this.EmitSignal("nameChanged");
	}

	private void mouse_entered() {
		isMouseInside = true;
	}
	private void mouse_exited() {
		isMouseInside = false;
	}

}
