using System;


public class GDD_ButtonNewTextArea: GDDMakerBaseButton
{
	Maker_UI owner;
	public GDD_ButtonNewTextArea() {
		name = "simpleTextArea";
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
