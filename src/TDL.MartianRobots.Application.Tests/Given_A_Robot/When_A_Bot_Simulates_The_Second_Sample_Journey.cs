using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Simulates_The_Second_Sample_Journey
{
    private readonly Robot _sut = new(new Pose(new Position(3, 2), Orientation.North));
    private readonly Grid _grid = new(new Position(5, 3));
    private readonly RobotCommand[] _commands =
    [
        RobotCommand.Forward, RobotCommand.Right, RobotCommand.Right,
        RobotCommand.Forward, RobotCommand.Left, RobotCommand.Left,
        RobotCommand.Forward, RobotCommand.Forward, RobotCommand.Right,
        RobotCommand.Right, RobotCommand.Forward, RobotCommand.Left,
        RobotCommand.Left
    ];

    [Fact]
    public void Then_The_Last_Valid_Pose_Is_Three_Three_North()
    {
        // Act
        var report = _sut.Simulate(_commands, _grid);

        // Assert
        report.EndPose.ShouldBe(new Pose(new Position(3, 3), Orientation.North));
    }

    [Fact]
    public void Then_The_Robot_Is_Lost()
    {
        // Act
        var report = _sut.Simulate(_commands, _grid);

        // Assert
        report.IsLost.ShouldBeTrue();
    }
}
