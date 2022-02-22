using Godot;
using System.Collections.Generic;
public class Maker_UI : CanvasLayer
{
	private GDD_ObjectNode selectedNode;
	private List<Button> buttons;

    [System.Obsolete]
    public void newTextArea(string name){
		GDD_Root root = GetOwner<GDD_Root>();
		name = root.addTextArea(name);


		addButtonToVBox(name);
	}
	
	public override void _Ready()
	{
		
	}

    [System.Obsolete]
    protected void addButtonToVBox(string name) {
		GDD_ObjectNode button = (GDD_ObjectNode) new GDD_ObjectNode().packedScene.Instance();
		button.reference = GetOwner<GDD_Root>().GetNodeOrNull<SimpleTextArea>(name);
		button.VPC = GetOwner<GDD_Root>().GetNode<GDD_ViewportContainer>("ViewportContainer");
		if(button != null)
			this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").AddChild(button);
		button.SetName(name);

		button.Connect("textAreaSelected", this, "textAreaSelected");
		button.Connect("textAreaDeselected", this, "textAreaDeselected");

	}

	private void textAreaSelected(GDD_ObjectNode node) {
		selectedNode = node;
		GD.Print("nodo selecionado " + node.Name);
	}

	private void textAreaDeselected(GDD_ObjectNode node) {
		selectedNode = null;		
	}

}
