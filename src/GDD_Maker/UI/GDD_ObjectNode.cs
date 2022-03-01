using Godot;
public class GDD_ObjectNode : Button
{
    [Signal]
    public delegate void textAreaSelected(GDD_ObjectNode n);
    [Signal]
    public delegate void textAreaDeselected(GDD_ObjectNode n);
    [Signal]
    public delegate void destroy(GDD_ObjectNode n);
    public string path;
    public int index = 1;
	public PackedScene packedScene;
    public Maker_UI parent;
    public Control reference;
    public Color color = new Color(1, 1 ,1);
    public GDD_ViewportContainer VPC;

    public GDD_ObjectNode() {
        path = "res://Assets/GDD_Maker/UI/GDD_ObjectNode.tscn";
		packedScene = GD.Load<PackedScene>(path);
    }

    [System.Obsolete]
    public override void _Ready()
    {
        SimpleTextArea refAsSTA = (SimpleTextArea) reference;
        refAsSTA.nodeReference = this;
        reference.Connect("nameChanged", this, "changeLabelText");
        reference.Connect("textAreaSelected", this, "TextAreaSelected");
        reference.Connect("textAreaDeselected", this, "TextAreaDeselected");
        reference.Connect("destroy", this, "Destroy");

        color = (reference as SimpleTextArea).titleColor;

        GetNode<Button>("Button").Connect("pressed", this, "moveToPosition");


        changeLabelText();
    }

    private void TextAreaSelected() {
		this.EmitSignal("textAreaSelected", this);
	}
    private void TextAreaDeselected() {
		this.EmitSignal("textAreaDeselected", this);
	}

    [System.Obsolete]
    public void changeLabelText() {
        GetNode<Label>("Label").SetText(reference.GetName());
        this.SetName(reference.GetName());
    }

    [System.Obsolete]
    public void moveToPosition() {
        VPC.SetPosition(new Vector2(reference.GetPosition().x - 400
        , reference.GetPosition().y - 100));
    }

    public void Destroy() {
        EmitSignal("destroy", this);
        this.QueueFree();
    }

    public void Link() {
        
    }

}
