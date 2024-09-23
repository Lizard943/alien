using Godot;
using System;
using System.Threading.Tasks;


public partial class world : Node2D
{
	public Camera2D camera2D;
	public Player player;
	public Events events;
	public Timer timer;
	public Vector2 PlayerSpawnScene = Vector2.Zero;
	public PackedScene PlayerScene = (PackedScene)ResourceLoader.Load("res://Player.tscn");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera2D = GetNode<Camera2D>("Camera2D");
		player = GetNode<Player>("Player");
		events = GetNode<Events>("/root/Events");
		timer = GetNode<Timer>("Timer");
		Color lb = new Color(Colors.LightBlue);
		RenderingServer.SetDefaultClearColor(lb);
		player.ConnectCamera(camera2D);
		PlayerSpawnScene = player.GlobalPosition;
		events.PlayerDied += OnPlayerDied;

	}

	/*public async Task Func2(){
		await timer.timeout;
	}*/
	public void OnPlayerDied(){
		//SomeFunction();
		//Fun2();
		Player player = (Player)PlayerScene.Instantiate();
		//GetNode("/root/World").CallDeferred("add_child",player);
		player.Position = PlayerSpawnScene;
		//GetNode("/root/World").AddChild(player);
		GetNode("/root/World").CallDeferred("add_child",player);
		player.ConnectCamera(camera2D);
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
