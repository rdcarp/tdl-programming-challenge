using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Repeats_A_Simulation
{
    [Fact]
    public void Then_Starts_From_The_Original_Pose()
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(2, 2), Orientation.North));
        RobotCommand[] commands = [RobotCommand.Forward, RobotCommand.Left, RobotCommand.Forward];
        sut.Simulate(commands, new Grid(new Position(5, 3)));

        // Act
        var report = sut.Simulate(commands, new Grid(new Position(5, 3)));

        // Assert
        report.EndPose.ShouldBe(new Pose(new Position(1, 3), Orientation.West));
    }
}
