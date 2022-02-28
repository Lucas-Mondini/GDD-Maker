using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;
public class Maker_UI : CanvasLayer
{
	private GDD_ObjectNode selectedNode;
	private List<GDD_ObjectNode> nodes;

	private int nodeCountSelect = 0;
	private bool SelectedNewNode = false;

    [System.Obsolete]
    public void newTextArea(string name){
		GDD_Root root = GetOwner<GDD_Root>();
		name = root.addTextArea(name);


		addButtonToVBox(name);
	}

	public void deleteTextArea() {
		selectedNode.Destroy();
	}

	public List<GDD_ObjectNode> getNodes() {
		return nodes;
	}

	public void removeNodeFromList(GDD_ObjectNode n) {

		List<GDD_ObjectNode> l = new List<GDD_ObjectNode>();
		foreach(GDD_ObjectNode i in nodes) {
			if(i != n)
				l.Add(i);
		}
		nodes.Clear();
		nodes = l;
	}
	
	public override void _Ready()
	{
		nodes = new List<GDD_ObjectNode>();
		
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
		button.Connect("destroy", this, "removeNodeFromList");

		nodes.Add(button);
		button.parent = this;

	}

    [System.Obsolete]
    private void textAreaSelected(GDD_ObjectNode node) {


		selectedNode = node;
		GD.Print("nodo selecionado " + node.Name);

		this.GetNode<Button>("HBoxContainer/ButtonDeleteTextArea").SetDisabled(false);
	}

    [System.Obsolete]
    private void textAreaDeselected(GDD_ObjectNode node) {
		Task.Delay(150).ContinueWith(_ => {
			selectedNode = null;		
			this.GetNode<Button>("HBoxContainer/ButtonDeleteTextArea").SetDisabled(true);
		});
	}
}
