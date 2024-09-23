using Godot;
using System;

public partial class Player : CharacterBody2D 
{
	[Export]
	public Stats Stat;
	
	
	enum Status {MOVE , CLIMB};
	public bool doublejump = true;
	Status state = Status.MOVE;
	
	public Events events;
	public SoundPlayer hurt;
	public AnimatedSprite2D _animatedsprite;
	public RayCast2D laddercheck;
	public AudioStreamPlayer audioStreamPlayer;
	public RemoteTransform2D remoteTransform2D;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	
	public override void _Ready()
	{
		_animatedsprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		laddercheck = GetNode<RayCast2D>("LadderCheck");
		audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		remoteTransform2D = GetNode<RemoteTransform2D>("RemoteTransform2D");
		hurt = GetNode<SoundPlayer>("/root/SoundPlayer");
		events = GetNode<Events>("/root/Events");

	}
	public void PlayerDie(){
		hurt.PlaySound();
		QueueFree();
		events.EmitSignal("PlayerDied"); 
		
		//events.EmitSignal(SignalName.PlayerDied); 
	}
	public bool IsOnLadder()
	{
		if (!laddercheck.IsColliding()) return false;
		Object collider = new Object();
		collider = laddercheck.GetCollider();
		if (collider.ToString() == "Ladder") return false;
		return true;
	}
	public void ConnectCamera(Camera2D cam){
		String CamPath = cam.GetPath();
		remoteTransform2D.RemotePath = CamPath;
	}
	public void MoveState(Vector2 direction, double delta)
	{
		
		if (IsOnLadder() && Input.IsActionPressed("ui_up")) state = Status.CLIMB;
		
		Vector2 velocity = Velocity;
		
		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
			_animatedsprite.Play("jump");
			
		}
		
		if (velocity == Vector2.Zero) {_animatedsprite.Play("default");}
		
		else if (Velocity.X > 0){ _animatedsprite.FlipH = true;}
		else if (Velocity.X < 0){ _animatedsprite.FlipH = false;}
		

		// Handle Jump.
		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("ui_up"))
			{
				velocity.Y = Stat.JumpVelocity;
				audioStreamPlayer.Play();
			}
			doublejump = true;
		}
		else 
		{
			if (Input.IsActionJustPressed("ui_up") && doublejump){
				velocity.Y = Stat.JumpVelocity;
				audioStreamPlayer.Play();
				doublejump = false;
			}
			
		}
		
		if (!IsOnFloor() && IsOnWall()){
			
			if (Input.IsActionJustPressed("ui_up"))
			{
				velocity.Y = Stat.JumpVelocity;
				audioStreamPlayer.Play();
			}
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		//Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Stat.Speed;
			_animatedsprite.Play("run");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Stat.Speed);
			_animatedsprite.Stop();
			
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	
	public void ClimbState(Vector2 direction)
	{
		if (!IsOnLadder()) state = Status.MOVE;
		Vector2 velocity = Velocity;
		velocity.X = direction.X * 70;
		velocity.Y = direction.Y * 70;
		if (velocity == Vector2.Zero) _animatedsprite.Stop();
		else _animatedsprite.Play();
		Velocity = velocity;
		MoveAndSlide();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		switch(state)
		{
			case Status.MOVE: {MoveState(direction,delta);break;}
			case Status.CLIMB: {ClimbState(direction);break;}
		}
	}
}




