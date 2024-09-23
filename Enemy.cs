using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	Vector2 velocity = Vector2.Zero;
	RayCast2D CheckLeft = new RayCast2D();
	RayCast2D CheckRight = new RayCast2D();
	AnimatedSprite2D sprite = new AnimatedSprite2D();
	Vector2 direction = Vector2.Right;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CheckLeft = GetNode<RayCast2D>("LegdeCheckLeft");
		CheckRight = GetNode<RayCast2D>("LegdeCheckRight");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		var IsOnLegde = !CheckLeft.IsColliding() || !CheckRight.IsColliding();
		if (IsOnWall() || IsOnLegde){
			direction *= -1;
		} 
		velocity = direction * 30;
		sprite.FlipH = direction.X > 0;
		sprite.Play();
		Velocity = velocity;
		MoveAndSlide();
	}
}
