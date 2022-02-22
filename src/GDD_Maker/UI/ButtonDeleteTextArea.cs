public class ButtonDeleteTextArea : GDDMakerBaseButton
{
	Maker_UI owner;
	public ButtonDeleteTextArea() {
		name = "simpleTextArea";
	}
	public override void _Ready()
	{
		owner = Owner as Maker_UI;
	}

    [System.Obsolete]
    protected new void _on_Button_pressed(){
		base._on_Button_pressed();
		owner.newTextArea(name);
	}
}
