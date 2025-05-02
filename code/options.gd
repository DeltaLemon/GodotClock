extends GridContainer
signal charge

var optionPath="res://option.tres"
# Called when the node enters the scene tree for the first time.
var option:options
var shutdown:CheckButton
var timestart:LineEdit
var timeover:LineEdit
var background_color:ColorPickerButton
var fonts_color:ColorPickerButton
var betop:CheckButton
var borderless:CheckButton
var dateformat:OptionButton
var topanel:CheckButton
var format:OptionButton
var local:OptionButton

func _ready():
	option=ResourceLoader.load(optionPath)
	get_window().size=Vector2i(350,600)
	shutdown=$TimeOver/TimeOver
	timestart=$OverTime/start
	timeover=$OverTime/end
	background_color=$ColorOption/color_back
	fonts_color=$ColorOption/color_fonts
	betop=$Top/BeTop
	borderless=$borderless/beborderless
	topanel=$topanel/topanel
	format=$dateformat2/format
	local=$weekformat/weekformat
	self.connect("charge",Callable(get_tree().root.get_node("/root/Global"),"_ready"))
	if (option.topanel):
		$"../..".modulate=option.color_fonts
		$"../../..".color=option.color_back
	shutdown.button_pressed=option.shutdown
	borderless.button_pressed=option.borderless
	betop.button_pressed=option.beTop
	background_color.color=option.color_back
	fonts_color.color=option.color_fonts
	timestart.text=option.alart_time["start"]
	timeover.text=option.alart_time["end"]
	topanel.button_pressed=option.topanel
	for f in option.formats:
		format.add_item(f)
	for r in option.regions:
		local.add_item(r)
	format.select(option.formats.find(option.format))
	local.select(option.regions.find(option.region))


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_save_pressed():
	option.shutdown=shutdown.button_pressed
	option.borderless=borderless.button_pressed
	option.beTop=betop.button_pressed
	option.color_back=background_color.color
	option.color_fonts=fonts_color.color
	option.alart_time["start"]=timestart.text
	option.alart_time["end"]=timeover.text
	option.topanel=topanel.button_pressed
	option.format=format.get_item_text(format.get_selected_id())
	option.region=local.get_item_text(local.get_selected_id())
	ResourceSaver.save(option,optionPath)
	emit_signal("charge")
	
	get_tree().change_scene_to_file("res://datetime.tscn")
