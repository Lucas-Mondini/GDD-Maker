using Godot;

public class ViewportContainer : Godot.ViewportContainer
{
    private float moveFactor = 20.0f;
	private bool isMouseDragBPressed = false;
    private Vector2 mouseDragInitialPosition;
	private Vector2 thisInitialPosition;

    [System.Obsolete]
    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.IsPressed() && eventMouseButton.ButtonIndex == ((int)ButtonList.Middle))
			{
				isMouseDragBPressed = true;
				mouseDragInitialPosition = GetViewport().GetMousePosition();
				thisInitialPosition = this.GetPosition();
			}
			else {
				isMouseDragBPressed = false;
			}
		}
    }

    [System.Obsolete]
    private void FollowMouse() {
		Vector2 newposition = new Vector2(
		((-GetViewport().GetMousePosition().x) + (mouseDragInitialPosition.x + thisInitialPosition.x)),
		((-GetViewport().GetMousePosition().y) + (mouseDragInitialPosition.y + thisInitialPosition.y))
		);

        GD.Print(newposition);
		this.SetPosition(newposition);

	}
    public override void _Ready()
    {

        
    }

    [System.Obsolete]
    public override void _Process(float delta) {
        if(isMouseDragBPressed){
			FollowMouse();
		}

    }
    

}
