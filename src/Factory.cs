using Godot;
using System;

public partial class Factory : Sprite2D
{
	public override void _Ready()
	{
		base._Ready();
		Texture = GD.Load<CompressedTexture2D>("res://res/factory.png");
	}

	public override void _Process(double delta)
	{
	}

	public void DepositFood()
	{
		Bot newBot = new Bot();
		newBot.GlobalPosition = GlobalPosition + Utils.RandVector2(100);
		AddSibling(newBot);
	}
}
