using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class Bot : Sprite2D
{
	public Vector2 Velocity = Vector2.Zero;
	private float _speed = 200;
	private float _range = 10;
	private bool _hasFood = false;
	private Node2D _target = null;

	public override void _Ready()
	{
		base._Ready();
		Texture = GD.Load<CompressedTexture2D>("res://res/bot.png");
	}

	public override void _Process(double delta)
	{
		if (_hasFood)
		{
			Return();
		}
		else
		{
			Hunt();
		}

		Position += Velocity * (float) delta;
	}

	private void Return()
	{
		var factory =  GetClosestInGroup<Factory>("Factory");
		if (factory == null) return;
		Velocity = GlobalPosition.DirectionTo(factory.GlobalPosition).Normalized() * _speed;

		if (GlobalPosition.DistanceTo(factory.GlobalPosition) > _range) return;
		factory.DepositFood();
		_hasFood = false;
		Velocity = Vector2.Zero;
	}

	private void Hunt()
	{
		_target = _target != null && IsInstanceValid(_target) ? _target : GetClosestInGroup<Food>("Food");
		if (_target == null)
		{
			Velocity = Vector2.Zero;
			return;
		}
		Velocity = GlobalPosition.DirectionTo(_target.GlobalPosition).Normalized() * _speed;

		if (GlobalPosition.DistanceTo(_target.GlobalPosition) > _range) return;
		_target.QueueFree();
		_target = null;
		_hasFood = true;
	}

	private float DistanceTo(Node2D node)
	{
		return GlobalPosition.DistanceTo(node.GlobalPosition);
	}

	private T GetClosestInGroup<T>(string groupName) where T : Node2D
	{
		var nodes = GetTree().GetNodesInGroup(groupName)
			.OfType<T>()
			.OrderBy(x => DistanceTo(x))
			.ToList();

		if (nodes.Count == 0) return null;
		
		// Choose a random node in the closest three nodes.
		var index = new Random().Next(0, nodes.Count > 3 ? 3 : nodes.Count);
		return nodes[index];
	}
}
