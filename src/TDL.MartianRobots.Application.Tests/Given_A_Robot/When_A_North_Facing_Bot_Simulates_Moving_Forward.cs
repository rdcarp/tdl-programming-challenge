using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_North_Facing_Bot_Simulates_Moving_Forward
{
    private const int StartX = 1;
    private const  int StartY = 1;
    private readonly Pose _startingPose = new Pose(new Position(StartX, StartY),  Orientation.North);

    private readonly Robot _sut;

    public When_A_North_Facing_Bot_Simulates_Moving_Forward()
    {
         _sut = new Robot(_startingPose);
    }
    
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    public void Then_Position_Is_Updated(int count, int expectedY)
    {
        // Arrange
        var commands = Enumerable.Repeat(RobotCommand.Forward, count);

        // Act
        var report = _sut.Simulate(commands);

        // Assert
        report.EndPose.Position.ShouldBe(new Position(StartX, expectedY));
    }

    [Fact]
    public void Then_Orientation_Remains_Unchanged()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Forward]);

        // Assert
        report.EndPose.Orientation.ShouldBe(_startingPose.Orientation);
    }
}
