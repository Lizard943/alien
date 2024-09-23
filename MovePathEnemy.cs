using Godot;
using System;

public partial class MovePathEnemy : Path2D
{
	
	// Called when the node enters the scene tree for the first time.
	public enum MoveType
	{
		LOOP,
		BOUNCE
	};
	[Export]
	public MoveType animationType;
	[Export]
	public int Speed = 1;
	
	public AnimationPlayer animationPlayer = new AnimationPlayer();
	
	public void PlayAnimation(){
		switch (animationType){
			case MoveType.LOOP: animationPlayer.Play("MoveAlongPath"); break;
			case MoveType.BOUNCE: animationPlayer.Play("MoveBounce"); break;
		}
	}
	
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		PlayAnimation();
		animationPlayer.SpeedScale = Speed;
	}	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
