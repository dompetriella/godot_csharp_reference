using Godot;

public partial class bg : ParallaxBackground
{
	[Export] CompressedTexture2D bgTexture;
	[Export] float scrollSpeed = 15.0f;
	Sprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = (Sprite2D)GetNode("ParallaxLayer/Sprite2D");
		bgTexture = GD.Load<CompressedTexture2D>(bgTexture.ResourcePath + bgTexture.ResourceName);
		sprite.Texture = bgTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 spritePosition = sprite.RegionRect.Position;
		Vector2 spriteSize = sprite.RegionRect.Size;
		Vector2 scrollVector = new Vector2(scrollSpeed, scrollSpeed);

		sprite.RegionRect = new Rect2(spritePosition + (float)delta * scrollVector, spriteSize);
		if (spritePosition >= new Vector2(64, 64))
		{
			sprite.RegionRect = new Rect2(Vector2.Zero, spriteSize);
		}
	}
}