using Godot;
using System;

public partial class SoundPlayer : Node
{
	public AudioStreamPlayer udioStreamPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		udioStreamPlayer = GetNode<AudioStreamPlayer>("SomeNoise");
	}
	public void PlaySound(){
		udioStreamPlayer.Play();
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
