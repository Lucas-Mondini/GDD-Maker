using Godot;

public class GDD_ObjectNode : Button
{
    public string path;
    public int index = 1;
	public PackedScene packedScene;
    public Control reference;
    public GDD_ViewportContainer VPC;

    public GDD_ObjectNode() {
        path = "res://Assets/GDD_Maker/UI/GDD_ObjectNode.tscn";
		packedScene = GD.Load<PackedScene>(path);
    }

    [System.Obsolete]
    public override void _Ready()
    {
        reference.Connect("nameChanged", this, "changeLabelText");
        changeLabelText();
    }

    [System.Obsolete]
    public void changeLabelText() {
        GetNode<Label>("Label").SetText(reference.GetName());
    }

    [System.Obsolete]
    protected void _pressed() {
        VPC.SetPosition(new Vector2(reference.GetPosition().x - 200
        , reference.GetPosition().y - 50));
    }

}
