using Godot;
using System;

public partial class FoodSpawner : CollisionPolygon2D
{
	private double _timeElapsed = 0;
	[Export] private double _timeToSpawn = 2;
	[Export] private int _max = 50;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_timeElapsed += delta;
		if (_timeElapsed > _timeToSpawn && GetChildCount() < _max)
		{
			_timeElapsed = 0;
			AddChild(new Food(GetRandomPoint()));
		}
	}

	private Vector2 GetRandomPoint()
	{
		var maxPoint = new Vector2(float.MinValue, float.MinValue);
		var minPoint = new Vector2(float.MaxValue, float.MaxValue);
		var polygon = Polygon;
		
		foreach (var point in polygon)
		{
			if (point.X > maxPoint.X) maxPoint.X = point.X;
			if (point.Y > maxPoint.Y) maxPoint.Y = point.Y;
			if (point.X < minPoint.X) minPoint.X = point.X;
			if (point.Y < minPoint.Y) minPoint.Y = point.Y;
		}

		for (var i = 0; i < 100; i++)
		{
			var x = Utils.RandRange(minPoint.X, maxPoint.X);
			var y = Utils.RandRange(minPoint.Y, maxPoint.Y);
	
			var randomPoint = new Vector2(x, y);
			
			if (Geometry2D.IsPointInPolygon(randomPoint, polygon))
			{
				return randomPoint;
			}
		}

		return Vector2.Zero;
	}
	

}
