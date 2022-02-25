using Godot;
using System.Collections.Generic;

public class GDD_Root : Control
{
    private int textAreaCount = 0;
    public List<GDD_ObjectNode> Nodes;
    public override void _Ready()
    {
        
    }

    [System.Obsolete]
    public string addTextArea(string name){
		GD.Print("Sapwning text area! " + textAreaCount +" - " + name );
        
        SimpleTextArea textArea = (SimpleTextArea) new SimpleTextArea().packedScene.Instance();
        textArea.name = ("SimpleTextArea_"+textAreaCount++);
        AddChild(textArea);

        
        return textArea.GetName();
	}

  public List<GDD_ObjectNode> getMakerUiNodes() {
    return Nodes;
  }
}
