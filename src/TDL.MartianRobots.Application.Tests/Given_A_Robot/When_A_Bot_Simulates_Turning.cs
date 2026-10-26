using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Simulates_Turning
{
    [Theory]
    [InlineData(Orientation.North, RobotCommand.Left, Orientation.West)]
    [InlineData(Orientation.West, RobotCommand.Left, Orientation.South)]
    [InlineData(Orientation.South, RobotCommand.Left, Orientation.East)]
    [InlineData(Orientation.East, RobotCommand.Left, Orientation.North)]
    [InlineData(Orientation.North, RobotCommand.Right, Orientation.East)]
    [InlineData(Orientation.East, RobotCommand.Right, Orientation.South)]
    [InlineData(Orientation.South, RobotCommand.Right, Orientation.West)]
    [InlineData(Orientation.West, RobotCommand.Right, Orientation.North)]
    public void Then_Orientation_Rotates_Ninety_Degrees(Orientation startingOrientation, RobotCommand command, Orientation expected)
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(2, 2), startingOrientation));

        // Act
        var report = sut.Simulate([command]);

        // Assert
        report.EndPose.Orientation.ShouldBe(expected);
    }

    [Theory]
    [InlineData(Orientation.North, RobotCommand.Left)]
    [InlineData(Orientation.East, RobotCommand.Left)]
    [InlineData(Orientation.South, RobotCommand.Left)]
    [InlineData(Orientation.West, RobotCommand.Left)]
    [InlineData(Orientation.North, RobotCommand.Right)]
    [InlineData(Orientation.East, RobotCommand.Right)]
    [InlineData(Orientation.South, RobotCommand.Right)]
    [InlineData(Orientation.West, RobotCommand.Right)]
    public void Then_Position_Remains_Unchanged(Orientation orientation, RobotCommand command)
    {
        // Arrange
        var startingPosition = new Position(2, 2);
        var sut = new Robot(new Pose(startingPosition, orientation));

        // Act
        var report = sut.Simulate([command]);

        // Assert
        report.EndPose.Position.ShouldBe(startingPosition);
    }
}
