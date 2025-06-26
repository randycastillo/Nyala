namespace Nyala.Core.Unit.Tests
{
    using FluentAssertions;
    using NSubstitute;
    using Nyala.Core;
    using Xunit;

    public class RoverTests
    {
        [Fact]
        public void ShouldInitiateRover()
        {
            var subject = new Rover(4);
            subject.Should().BeOfType<Rover>();
        }

        [Fact]
        public void ShouldInitiateWithStartingPoint()
        {
            var subject = new Rover(4);
            var coordinates = subject.GetCurrentLocation();
            coordinates.Should().Be($"0,0 facing North");
        }

        [Fact]
        public void ShouldReceiveArrayOfCommands()
        {
            var subject = Substitute.For<IRover>();
            subject.Command("f,b,l,r");
            subject.Received().Command(Arg.Is<string>(x => x == "f,b,l,r"));
            ; }

        [Fact]
        public void ShouldMoveForwardWhenCommandIsF()
        {
            var subject = new Rover(5);
            subject.Command("f");
            subject.GetCurrentLocation().Should().Be($"0,1 facing North");
        }

        [Theory]
        [InlineData("f,f,l,f", 5, 3)]
        [InlineData("b,b,r,b", 1, 3)]
        public void ShouldSetCorrectCoordinatesAfterSendingMulitpleCommands(string commands, int expectedX, int expectedY)
        {
            var subject = new Rover(5);
            subject.Command(commands);
            subject.XCoordinate.Should().Be(expectedX);
            subject.YCoordinate.Should().Be(expectedY);
        }

        [Theory]
        [InlineData("f", DirectionEnum.North)]
        [InlineData("b", DirectionEnum.South)]
        [InlineData("l", DirectionEnum.West)]
        [InlineData("r", DirectionEnum.East)]
        public void ShouldHaveCorrectFacingDirectionAfterSendingCommands(string command, DirectionEnum expectedDirection)
        {
            var subject = new Rover(5);
            subject.Command(command);
            subject.Direction.Should().Be(expectedDirection);
        }

        [Fact]
        public void ShouldSetGridSize()
        {
            var subject = new Rover(5);
            subject.GridSize.Should().Be(5);
        }

        [Theory]
        [InlineData("f,f,l,l,f,f,l,l,f", 2, 2)]
        [InlineData("b,b,r,r,b,b,r,r,b", 1, 1)]
        [InlineData("f,f,r,r,f,l,l,l,l,l,l,l,l,b", 0, 2)]
        public void ShouldNotGetOutsideTheGridAfterMoving(string commands, int expectedX, int expectedY)
        {
            var subject = new Rover(2);
            subject.Command(commands);
            subject.XCoordinate.Should().Be(expectedX);
            subject.YCoordinate.Should().Be(expectedY);
        }

        [Theory]
        [InlineData("f", 0, 1)]
        [InlineData("b", 0, 5)]
        [InlineData("l", 5, 0)]
        [InlineData("r", 1, 0)]
        [InlineData("f,r,r", 1, 1)]
        public void ShouldDetectObstacleBeforeMoving(string commands, int xObstacle, int yObstacle)
        {
            var subject = new Rover(5);
            subject.Obtacles = new List<(int, int)>()
            {
                (xObstacle, yObstacle)
            };

            Assert.Throws<ObstacleDetectedException>(
                () => subject.Command(commands));
        }
    }
}
