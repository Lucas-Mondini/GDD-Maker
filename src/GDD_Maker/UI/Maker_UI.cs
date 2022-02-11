using Godot;

public class Maker_UI : Control
{

    [System.Obsolete]
    public void newTextArea(string name){
		GDD_Root root = GetOwner<GDD_Root>();
		root.addTextArea(name);
	}
	
	public override void _Ready()
	{
		
	}
}
