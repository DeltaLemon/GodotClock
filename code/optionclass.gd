extends Resource

class_name options
var formats:Array=[
	"yyyy/MM/dd",
	"yyyy-MM-dd",
	"yyyy.MM.dd"
	]
var regions:Array=[
	"en-US",
	"zh-chs",
	"zh-cht"
]
@export var shutdown:bool
@export var alart_time:Dictionary={
	"start": "",
	"end": ""
}
@export var borderless:bool
@export var topanel:bool
@export var color_back:Color
@export var color_fonts:Color
@export var beTop:bool
@export var transparent:bool
@export var format:String
@export var region:String
