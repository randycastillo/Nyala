Feature: MarsRover Expedition

Scenario: MarsRover is initialized
Given Rover is landed on Mars with default coordinates
Then Rover coordinates should be 0,0 and facing North

Scenario: MarsRover received commands
Given Rover is landed on Mars with default coordinates
When I send commands f,f,f,f
Then Rover coordinates should be 0,4 and facing North 
When I send commands f,f,f,r
Then Rover coordinates should be 1,1 and facing East 

Scenario: MarsRover ecountered obstacles
Given Rover is landed on Mars with default coordinates
And Mars has obstacle at coordinates 0,1
When I send commands f
Then Error should be raised
And Rover coordinates should be 0,0 and facing North
