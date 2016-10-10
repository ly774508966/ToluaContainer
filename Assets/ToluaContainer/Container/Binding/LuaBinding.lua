package.path = package.path .. ";?.lua"
local requiredpackage = require("LuaBaseType")

LuaBinding = class()

function LuaBinding:ctor(binder, type, bindingType, constraint)	-- 构造函数
	self.binder = binder
	self.type = type
	self.value = {}
	self.id = nil
	self.constraint = constraint
	self.bindingType = bindingType
	self.condition = nil
end
 
function LuaBinding:ToAddressAtLua(constraint, value)
	self.constraint = constraint
	self.value = value
end
 
function LuaBinding:hello()	-- 定义另一个成员函数 base_type:hello
	print("hello base_type")
end


--[[ 
]]--
a = LuaBinding.new(1,2,3,4)

print(a.type)