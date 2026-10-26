using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Simulates_Moving_Forward
{
    [Theory]
    [InlineData(Orientation.North, 2, 3)]
    [InlineData(Orientation.East, 3, 2)]
    [InlineData(Orientation.South, 2, 1)]
    [InlineData(Orientation.West, 1, 2)]
    public void Then_Position_Is_Updated_In_The_Facing_Direction(Orientation orientation, int expectedX, int expectedY)
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(2, 2), orientation));

        // Act
        var report = sut.Simulate([RobotCommand.Forward]);

        // Assert
        report.EndPose.Position.ShouldBe(new Position(expectedX, expectedY));
    }

    [Theory]
    [InlineData(Orientation.North)]
    [InlineData(Orientation.East)]
    [InlineData(Orientation.South)]
    [InlineData(Orientation.West)]
    public void Then_Orientation_Remains_Unchanged(Orientation orientation)
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(2, 2), orientation));

        // Act
        var report = sut.Simulate([RobotCommand.Forward]);

        // Assert
        report.EndPose.Orientation.ShouldBe(orientation);
    }
}
