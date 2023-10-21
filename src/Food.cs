using Godot;
using System;

public partial class Food : Sprite2D
{

	public Food()
	{
		
	}

	public override void _Ready()
	{
		base._Ready();
		AddToGroup("Food");
	}

	public Food(Vector2 pos)
	{
		GD.Print("spawning at pos ", pos.ToString());
		GlobalPosition = pos;
		Texture = GD.Load<CompressedTexture2D>("res://res/food.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
