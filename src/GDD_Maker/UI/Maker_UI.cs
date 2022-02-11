using Godot;

public class Maker_UI : Control
{

    [System.Obsolete]
    public void newTextArea(string name){
		GDD_Root root = GetOwner<GDD_Root>();
		SimpleTextArea textArea = new SimpleTextArea();
		root.addTextArea(name, textArea);

	}
	
	public override void _Ready()
	{
		
	}
}
