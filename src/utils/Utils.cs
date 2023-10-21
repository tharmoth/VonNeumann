using Godot;
using System;

public partial class Utils : Object
{
	public static float RandRange(float min, float max)
	{
		return new Random().NextSingle() * (max - min) + min;
	}

	public static Vector2 RandVector2(float radius)
	{
		return new Vector2(RandRange(-radius, radius), RandRange(-radius, radius)).Normalized() * radius;
	}
	
}
