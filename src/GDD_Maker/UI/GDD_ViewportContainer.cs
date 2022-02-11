using Godot;

public class GDD_ViewportContainer : Godot.ViewportContainer
{
    private float moveFactor = 20.0f;
	private float zoomFactor = 0.1f;

	private const float MIN_SIZE = 1.0f;
	private const float MAX_SIZE = 20.0f;

	private bool isMouseDragBPressed = false;
    private Vector2 mouseDragInitialPosition;
	private Vector2 thisInitialPosition;

    [System.Obsolete]
    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.IsPressed())
			{
				if(eventMouseButton.ButtonIndex == ((int)ButtonList.Middle)) {
					isMouseDragBPressed = true;
					mouseDragInitialPosition = GetViewport().GetMousePosition();
					thisInitialPosition = this.GetPosition();
				}
				if (eventMouseButton.ButtonIndex == ((int)ButtonList.WheelUp)) {
					GD.Print("zoomOut");
					this.ZoomIn();
				}

				if (eventMouseButton.ButtonIndex == ((int)ButtonList.WheelDown)) {
					GD.Print("zoomIn");
					this.ZoomOut();
				}
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

	private void ZoomOut() {
		Vector2 newScale = new Vector2(this.RectScale.x + zoomFactor, this.RectScale.y + zoomFactor);
		if(newScale.x <= MAX_SIZE) {
			this.RectScale = newScale;
			GetNode<GDD_Camera2D>("GDD_Camera2D").Zoom = new Vector2(this.RectScale);
		}
		this.moveFactor = moveFactor + RectScale.x;
	}

	private void ZoomIn() {
		Vector2 newScale = new Vector2(this.RectScale.x - zoomFactor, this.RectScale.y - zoomFactor);
		if(newScale.x >= MIN_SIZE) {
			this.RectScale = newScale;
			GetNode<GDD_Camera2D>("GDD_Camera2D").Zoom = new Vector2(this.RectScale);
		}
		this.moveFactor = moveFactor + RectScale.x;

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
