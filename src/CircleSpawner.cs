using Godot;
using System;

[Tool]
public partial class CircleSpawner : Node2D
{

	[Export] private float _innerRadius = 7500;
	[Export] private float _outerRadius = 10000;
	[Export] private int _startingFoodCount = 1000;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < _startingFoodCount; i++)
		{
			Food food = new Food(GetRandomPoint());
			CallDeferred("add_sibling", food);
		}
	}

	private Vector2 GetRandomPoint()
	{
		var angleRadians = Utils.RandRange(0, 2 * (float)Math.PI);
		var radius = Utils.RandRange(_innerRadius, _outerRadius);

		var x = radius * (float) Math.Cos(angleRadians);
		var y = radius * (float) Math.Sin(angleRadians);
		
		GD.Print(x, " " ,y);
		
		return new Vector2(x, y);
	}
	
	
	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }
}
