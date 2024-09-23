using Godot;
using System;


[GlobalClass]
public partial class Stats : Resource
{
	[Export]
	public float Speed = 80.0f;
	[Export]
	public float JumpVelocity = -250.0f;
}
