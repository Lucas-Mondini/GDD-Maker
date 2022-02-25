public class SimpleTextAreaDeleteButton : GDDMakerBaseButton
{
	SimpleTextArea owner;
	public SimpleTextAreaDeleteButton() {
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
		owner.Destroy();
	}
}
