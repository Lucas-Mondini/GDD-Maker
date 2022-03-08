using Godot;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

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
	public List<Button> nodes = new List<Button>();
	public List<SimpleTextArea> LinkedNodes = new List<SimpleTextArea>();
	public Color titleColor = new Color(1, 1, 1);
	public Hashtable links = new Hashtable();
	private List<string> linkedWords = new List<string>();
	private SimpleTextArea lastNode_From = null;

	public SimpleTextArea()
	{
		path = "res://Assets/GDD_Maker/UI/SimpleTextArea.tscn";
		packedScene = GD.Load<PackedScene>(path);
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

		hidePanel();

		GetNode<Button>("ButtonsContainer/SimpleTextAreaDisplayLinkedNodes").Connect("pressed", this, "displayLinkedNodes");
		GetNode<Button>("ButtonsContainer/SimpleTextAreaGoToLinkedNode").Connect("pressed", this, "");

		

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
    private void FollowMouse() {
		Vector2 newposition = new Vector2(
		(GetViewport().GetMousePosition().x - mouseDrag.x + thisInitialPosition.x),
		(GetViewport().GetMousePosition().y - mouseDrag.y + thisInitialPosition.y)
		);
		this.SetPosition(newposition);

	}

	public void Destroy() {
		EmitSignal("destroy");
		QueueFree();
	}
	private void hidePanel() {
		GetNode<Panel>("Panel").Visible = false;
	}

	public void setTitleColor(Color c) {
		textEditTitle.AddColorOverride("font_color", c);
		titleColor = c;
		nodeReference.color = c;
	}

	public void gotLinked(Color c, SimpleTextArea from) {
		setTitleColor(c);
		bool isInList = false;
		LinkedNodes.ForEach(_=> {
			if(from == _)
				isInList = true;
		});
		if(!isInList)
			LinkedNodes.Add(from);
		lastNode_From = from;
	}

    [Obsolete]
    private void LinkNode(SimpleTextArea STA) {
		string text = textEditBody.GetSelectionText();

		Color c = STA.titleColor;
		if(c == new Color(1, 1, 1)) {
		c = new Color((float) new Random().NextDouble(), (float) new Random().NextDouble(), (float) new Random().NextDouble());
			if(c == new Color(1, 1, 1))
				c = new Color(1, 0, 0);
		}
		

		textEditBody.AddKeywordColor(text, c);
		hidePanel();

		STA.gotLinked(c, this);
		STA.nodeReference.moveToPosition();

		Action goToReference = STA.nodeReference.moveToPosition;
		links.Add(text, goToReference);
		linkedWords.Add(text);
	}


    [Obsolete]
    private void updateTextAreaNodes() {
		Godot.Collections.Array buttons = this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").GetChildren();
		foreach(Button b in buttons) {
			b.QueueFree();
		}

		List<GDD_ObjectNode> on = nodeReference.parent.getNodes();
		nodes.Clear();
		foreach (GDD_ObjectNode item in on)
		{
			Button node = new Button();
			Label label = new Label();
			node.AddChild(label);
			label.SetText(item.GetName());
			label.AddColorOverride("font_color", item.color);


			Godot.Collections.Array arr = new Godot.Collections.Array();
			arr.Add(item.reference);
			node.Connect("pressed", this, "LinkNode", arr);
			node.Connect("pressed", item, "moveToPosition");
			if(item.reference != this)
				nodes.Add(node);
		}
	}

    [Obsolete]
    private void updateTextAreaNodesLinkedOnly() {
		Godot.Collections.Array buttons = this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").GetChildren();

		foreach(Button b in buttons) {
			b.QueueFree();
		}
		nodes.Clear();
		List<SimpleTextArea> on = LinkedNodes;


		foreach (SimpleTextArea item in on)
		{
			Button node = new Button();
			Label label = new Label();
			node.AddChild(label);
			label.SetText(item.GetName());
			label.AddColorOverride("font_color", item.titleColor);

			node.Connect("pressed", item.nodeReference, "moveToPosition");
			node.Connect("pressed", this, "hidePanel");
			this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").AddChild(node);
		}
	}

    [Obsolete]
    private void displayLinkedNodes() {
		Panel p = GetNode<Panel>("Panel");
			p.Visible = true;

			updateTextAreaNodesLinkedOnly();

			foreach(Button node in nodes)
				this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").AddChild(node);
	}
	    
    [Obsolete]
    public void LinkSelection() {
		string text = textEditBody.GetSelectionText();

		if ( Regex.IsMatch( text, @"^[A-Za-z]+$" )) {
			Panel p = GetNode<Panel>("Panel");
			p.Visible = true;

			updateTextAreaNodes();

			foreach(Button node in nodes)
				this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").AddChild(node);

		}
		else 
			GD.PrintErr("Selection is not a word");
		
	}

	private void goToLinkedNode() {
		linkedWords.ForEach(s => {
			if(textEditBody.GetWordUnderCursor() == s) {
				((Action) links[s])();
			}
		});
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
			if (eventMouseButton.IsPressed()) 
			{
				switch(eventMouseButton.ButtonIndex) {
					case (int)ButtonList.Middle:
						isMouseDragBPressed = true;
						mouseDrag = GetViewport().GetMousePosition();
						thisInitialPosition = this.GetPosition();
						break;
					case (int)ButtonList.Left :
						goToLinkedNode();
						break;
				}
				
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
