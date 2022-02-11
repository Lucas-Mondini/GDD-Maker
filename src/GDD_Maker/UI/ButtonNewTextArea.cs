using System;


public class ButtonNewTextArea: GDDMakerBaseButton
{
	Maker_UI owner;
	public ButtonNewTextArea() {
		name = "new Text Area";
	}
	public override void _Ready()
	{
		owner = Owner as Maker_UI;
	}

    [Obsolete]
    protected new void _on_Button_pressed(){
		base._on_Button_pressed();
		owner.newTextArea(name);
	}

}
