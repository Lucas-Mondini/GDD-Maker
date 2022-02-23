using Godot;
using System.Threading.Tasks;
public class Maker_UI : CanvasLayer
{
	private GDD_ObjectNode selectedNode;

	private int nodeCountSelect = 0;
	private bool SelectedNewNode = false;

    [System.Obsolete]
    public void newTextArea(string name){
		GDD_Root root = GetOwner<GDD_Root>();
		name = root.addTextArea(name);


		addButtonToVBox(name);
	}

	public void deleteTextArea() {
		selectedNode.destroy();
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
