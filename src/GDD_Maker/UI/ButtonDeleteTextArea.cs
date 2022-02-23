public class ButtonDeleteTextArea : GDDMakerBaseButton
{
	Maker_UI owner;
	public ButtonDeleteTextArea() {
		name = "simpleTextArea";
	}

    [System.Obsolete]
    public override void _Ready()
	{
		owner = Owner as Maker_UI;
        this.Connect("pressed", this, "_on_Button_pressed");
        this.SetDisabled(true);

	}

    [System.Obsolete]
    protected new void _on_Button_pressed(){
		base._on_Button_pressed();
		owner.deleteTextArea();
	}
}
