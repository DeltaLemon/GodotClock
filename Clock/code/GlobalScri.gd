extends Control
var if_click:bool = false
var start_pos:Vector2i
var win_start:Vector2i
var optionPath:String="res://option.tres"
var option:options
var winroot:Window
var colorback:Color
var texturecolor:Color
var format:String
var region:String
#var themed:Theme  themed=ResourceLoader.load("res://main.tres")
#themed.set_color("font_color","Label",option.color_fonts)
# Called when the node enters the scene tree for the first time.
func _ready():
	option=ResourceLoader.load(optionPath)
	
	winroot=get_tree().root
	winroot.set_transparent_background(true)
	winroot.always_on_top=option.beTop
	winroot.borderless=option.borderless
	colorback=option.color_back
	texturecolor=option.color_fonts
	format=option.format
	region=option.region
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func  _input(event):
	if event is InputEventMouseButton and event.is_action_pressed("Clock_click") and !if_click:
		self.if_click=true
		start_pos=DisplayServer.mouse_get_position()
		win_start=winroot.position
	if event is InputEventMouseButton and !event.is_action_pressed("Clock_click") :
		self.if_click=false
		
	if if_click:
		winroot.position=(DisplayServer.mouse_get_position()-start_pos)+win_start
		

func _process(delta):
	var now=Time.get_unix_time_from_datetime_string(Time.get_datetime_string_from_system())
	var today=Time.get_date_string_from_system()
	var s=Time.get_unix_time_from_datetime_string(today+"T"+ option.alart_time["start"])
	var e=Time.get_unix_time_from_datetime_string(today+"T"+ option.alart_time["end"])
	if(e>=s):
		if(now>s and now<e):
			
			if(option.shutdown):
				shutdown()
	else:
		if(now>s or now<e):
			
			if(option.shutdown):
				shutdown()
	

func shutdown():
	OS.execute("shutdown",["-s"])#Window
	OS.execute("sudo shutdown",["-h","now"])#MacOS
	OS.execute("poweroff",[""])#Linux
