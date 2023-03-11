using Godot;
using System;
using System.Globalization;

public partial class jktime : Control
{
	private System.DateTime currentTime=new System.DateTime();
	private Label time;
	private Label date;
	private Label week;
	private ColorRect back;
	private String start;
	private String end;
	private Control Global;
	private TextureButton option;
	private String format;
	private String region;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		GetWindow().Size=new Vector2I(350,130);
		Callable callable = new Callable(this, MethodName.Colorback);
		time=GetNode<Label>("time");//$time
		date=GetNode<Label>("time/date");//$time/date
		week=GetNode<Label>("time/date/week");//$time/date/week
		Global=GetTree().Root.GetNode<Control>("Global");
		back=GetNode<ColorRect>("backgound");//$backgound
		option=GetNode<TextureButton>("time/option");//$time/option
		back.Color=(Color) Global.Get("colorback");
		time.Modulate=(Color) Global.Get("texturecolor");
		format=(String)Global.Get("format");
		region=(String)Global.Get("region");
		//DisplayServer.WindowSetMousePassthrough(GetNode<Polygon2D>("Polygon2D").Polygon);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		currentTime=System.DateTime.Now;
		date.Text=currentTime.ToString(format);
		time.Text=currentTime.ToString("HH:mm:ss");
		week.Text=currentTime.ToString("ddd",new CultureInfo(region));
	}
	private void Colorback(Color c){
		back.Color=c;
	}
	private void _on_option_pressed()
	{
		GetTree().ChangeSceneToFile("res://options.tscn");
	}
}



