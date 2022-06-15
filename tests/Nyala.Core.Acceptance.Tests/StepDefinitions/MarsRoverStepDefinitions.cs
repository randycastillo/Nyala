namespace Nyala.Core.Acceptance.Tests.StepDefinitions
{
    using Nyala.Core;
    using TechTalk.SpecFlow;
    using FluentAssertions;

    [Binding]
    public sealed class MarsRoverStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private const int defaultGridSize = 5;
        private IRover rover;
        private Exception actualException;

        public MarsRoverStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"Rover is landed on Mars with default coordinates")]
        public void GivenRoverIsLandedOnMarsWithDefaultCoordinates()
        {
            this.rover = new Rover(defaultGridSize);
        }

        [Given(@"Mars has obstacle at coordinates (.*),(.*)")]
        public void MarsHasObstacleAtCoordinates(int x, int y)
        {
            this.rover.Obtacles = new List<(int, int)>()
            {
                (x, y)
            };
        }

        [Then(@"Rover coordinates should be (.*),(.*) and facing (.*)")]
        public void RoverCoordinatesShouldBe(int xCoordinate, int yCoordinate, DirectionEnum direction)
        {
            var roverLocation = $"{xCoordinate},{yCoordinate} facing {direction}";
            this.rover.GetCurrentCoordinates().Should().Be(roverLocation);
        }

        [When(@"I send commands (.*)")]
        public void ISendCommands(string commands)
        {
            try
            {
                this.rover.Command(commands);

            }
            catch (ObstacleDetectedException ex)
            {

                this.actualException = ex;
            }
        }

        [Then(@"Error should be raised")]
        public void ThenErrorShouldBeRaised()
        {
            this.actualException.Should().BeOfType<ObstacleDetectedException>();
            this.actualException.Message.Should()
                .Be("Obstacle is detected. Cannot proceed. CurrentLocation 0,0 facing North.");
        }
    }
}
