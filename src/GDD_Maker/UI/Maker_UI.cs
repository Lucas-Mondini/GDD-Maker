using Godot;

public class Maker_UI : Control
{

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

		button = this.GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer").GetNode<GDD_ObjectNode>("GDD_ObjectNode");
		button.SetName(name);
		
	}
}
