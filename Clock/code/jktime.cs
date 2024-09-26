using Godot;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
public partial class jktime : Control
{
#if WINDOWS
	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
	[DllImport("user32.dll")]	
	private static extern IntPtr GetActiveWindow();
	[DllImport("user32.dll")]
	public static extern bool GetCursorPos(out POINT lpPoint);
	[StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
	private const int GWL_EXSTYLE=-20;
	private const int WS_EX_TRANSPARENT=0x00000020;
	private const int WS_EX_LAYERED=0x00080000;
	private IntPtr hWnd;

#endif
	
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
		time=GetNode<Label>("time");//$time
		date=GetNode<Label>("time/date");//$time/date
		week=GetNode<Label>("time/date/week");//$time/date/week
		Global=GetTree().Root.GetNode<Control>("Global");
		GetTree().Root.TransparentBg=true;
		back=GetNode<ColorRect>("backgound");//$backgound
		option=GetNode<TextureButton>("time/option");//$time/option
		//全局脚本节点option
		back.Color=(Color) Global.Get("colorback");//获取背景颜色
		time.Modulate=(Color) Global.Get("texturecolor");//获取字体颜色
		format=(String)Global.Get("format");//日期格式
		region=(String)Global.Get("region");//地区代码

		MousePassthrough(true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		MousePassthrough();
		currentTime=DateTime.Now;
		date.Text=currentTime.ToString(format);//根据日期格式获取日期字符串
		time.Text=currentTime.ToString("HH:mm:ss");//获取时间字符串
		week.Text=currentTime.ToString("ddd",new CultureInfo(region));//根据地区代码获取周日字符串
	}
	

	public void MousePassthrough(bool value=true){
	value=value && (bool)Global.Get("clickthrough");
	#if LINUX || MACOS
		if (!value||Input.IsKeyPressed(Key.Ctrl))
			GetWindow().MousePassthroughPolygon=new Vector2[0]; 
		else
			GetWindow().MousePassthroughPolygon=GetNode<Polygon2D>("Polygon2D").Polygon; //设置鼠标穿透区域
	#endif

	#if WINDOWS
		POINT p;
		if (GetCursorPos(out p)){
			hWnd=GetActiveWindow();
			Vector2 mousePosition = new(p.X, p.Y);
			Polygon2D polygon2D = GetNode<Polygon2D>("Polygon2D");
			if (!value
				||Geometry2D.IsPointInPolygon(mousePosition-DisplayServer.WindowGetPosition(), polygon2D.Polygon)
				||Input.IsKeyPressed(Key.Ctrl))
				SetWindowLong(hWnd,GWL_EXSTYLE,WS_EX_LAYERED);
			else
				SetWindowLong(hWnd,GWL_EXSTYLE,WS_EX_LAYERED|WS_EX_TRANSPARENT);
		}
		
	#endif
	}
	private void _on_option_pressed()
	{
		GetTree().ChangeSceneToFile("res://options.tscn");
	}
	
}



