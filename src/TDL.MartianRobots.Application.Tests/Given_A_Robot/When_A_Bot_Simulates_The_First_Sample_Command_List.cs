using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Simulates_The_First_Sample_Command_List
{
    private readonly Robot _sut = new(new Pose(new Position(1, 1), Orientation.East));
    private readonly RobotCommand[] _commands =
    [
        RobotCommand.Right, RobotCommand.Forward,
        RobotCommand.Right, RobotCommand.Forward,
        RobotCommand.Right, RobotCommand.Forward,
        RobotCommand.Right, RobotCommand.Forward
    ];

    [Fact]
    public void Then_Returns_To_The_Starting_Pose()
    {
        // Act
        var report = _sut.Simulate(_commands, new Grid(new Position(5, 3)));

        // Assert
        report.EndPose.ShouldBe(new Pose(new Position(1, 1), Orientation.East));
    }

}
