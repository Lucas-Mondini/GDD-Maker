public class SimpleTextAreaLinkButton : GDDMakerBaseButton
{
	SimpleTextArea owner;
	public SimpleTextAreaLinkButton() {
		name = "simpleTextArea";
	}
	public override void _Ready()
	{
		owner = Owner as SimpleTextArea;
        this.Connect("pressed", this, "_on_Button_pressed");
	}

    [System.Obsolete]
    protected new void _on_Button_pressed(){
		base._on_Button_pressed();
		owner.LinkSelection();
	}
}
