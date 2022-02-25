using Godot;
using System;
using System.Collections.Generic;

public class SimpleTextArea : Control
{
	[Signal]
	public delegate void nameChanged();
	[Signal]
	public delegate void textAreaSelected();
	[Signal]
	public delegate void textAreaDeselected();
	[Signal]
	public delegate void destroy();
	public string path;
	public string name;
	public PackedScene packedScene;


	private TextEdit textEditBody;
	private TextEdit textEditTitle;

	private List<String> signals;

	
	public int index = -1;
	private bool isMouseInside = false;
	private bool isMouseDragBPressed = false;

	private Vector2 mouseDrag;
	private Vector2 thisInitialPosition;
	public GDD_ObjectNode nodeReference;

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

		GetNode<Button>("ButtonsContainer/SimpleTextAreaGoToLinkedNode").SetDisabled(true);

		GetNode<Panel>("Panel").Visible = false;

		

		textEditBody = this.GetNode<TextEdit>("TextEdit");
		textEditBody.Connect("gui_input", this, "InputProcess");
		textEditBody.Connect("mouse_entered", this, "mouse_entered");
		textEditBody.Connect("mouse_exited", this, "mouse_exited");
		textEditBody.Connect("focus_entered", this, "TextAreaSelected");
		textEditBody.Connect("focus_exited", this, "TextAreaDeselected");



		textEditTitle = this.GetNode<TextEdit>("Title");
		textEditTitle.Connect("text_changed", this, "setTitleName");
		textEditTitle.Connect("focus_entered", this, "TextAreaSelected");
		textEditTitle.Connect("focus_exited", this, "TextAreaDeselected");
		textEditTitle.SetText(name);

		SetName(name);
		GD.Print(name);
	}

	public void Destroy() {
		EmitSignal("destroy");
	}

    [Obsolete]
    public void LinkSelection() {
		string text = textEditBody.GetSelectionText();
		textEditBody.AddKeywordColor(text, new  Color(0.5f, 0f, 0.5f));


		Panel p = GetNode<Panel>("Panel");
		p.Visible = true;


		//TODO
		VBoxContainer vbc = p.GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");
		List<GDD_ObjectNode> on = nodeReference.parent.getNodes();
		foreach (GDD_ObjectNode item in on)
		{
			vbc.AddChild(item);
		}

		GD.Print(vbc.GetChildCount());
		
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
