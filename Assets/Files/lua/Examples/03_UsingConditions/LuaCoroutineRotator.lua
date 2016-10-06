transform = nil

function Rotator()
	transform:Rotate(-1.0, -1.0, -1.0)
end

local coDelay = nil

function Delay()
	while true do
		coroutine.wait(0.05) 
		Rotator()
	end
end

function StartDelay()
	coDelay = coroutine.start(Delay)
end
