using Godot;
using System.Collections.Generic;

public class GDD_Root : Control
{
    private int textAreaCount = 0;
    private List<SimpleTextArea> textAreaList;
    public override void _Ready()
    {
        
    }

        public void addTextArea(string name){
        SimpleTextArea textArea = new SimpleTextArea();
		GD.Print("Sapwning text area! " + textAreaCount++ +" - " + name );

		CallDeferred("add_child", textArea.packedScene.Instance());
		//textAreaList.Add(textArea);

	}
}
