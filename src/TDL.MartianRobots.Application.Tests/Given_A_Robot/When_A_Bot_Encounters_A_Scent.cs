using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Encounters_A_Scent
{
    private readonly Grid _grid = new(new Position(5, 3));
    private readonly Robot _sut = new(new Pose(new Position(5, 3), Orientation.North));

    public When_A_Bot_Encounters_A_Scent()
    {
        var lostRobot = new Robot(new Pose(new Position(5, 3), Orientation.North));
        lostRobot.Simulate([RobotCommand.Forward], _grid);
    }

    [Fact]
    public void Then_Repeated_Unsafe_Moves_Are_Ignored()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Forward, RobotCommand.Forward], _grid);

        // Assert
        report.IsLost.ShouldBeFalse();
    }

    [Fact]
    public void Then_The_Position_Is_Unchanged_By_An_Ignored_Move()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Forward], _grid);

        // Assert
        report.EndPose.Position.ShouldBe(new Position(5, 3));
    }

    [Fact]
    public void Then_Remaining_Turns_And_Safe_Moves_Are_Executed()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Forward, RobotCommand.Left, RobotCommand.Forward], _grid);

        // Assert
        report.EndPose.ShouldBe(new Pose(new Position(4, 3), Orientation.West));
    }

    [Fact]
    public void Then_An_Unscented_Edge_At_The_Same_Coordinate_Still_Allows_Loss()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Right, RobotCommand.Forward], _grid);

        // Assert
        report.IsLost.ShouldBeTrue();
    }

    [Fact]
    public void Then_A_North_Facing_Robot_Moves_Safely_Past_An_East_Edge_Scent()
    {
        // Arrange
        var lostRobot = new Robot(new Pose(new Position(5, 1), Orientation.East));
        lostRobot.Simulate([RobotCommand.Forward], _grid);
        var sut = new Robot(new Pose(new Position(5, 1), Orientation.North));

        // Act
        var report = sut.Simulate([RobotCommand.Forward], _grid);

        // Assert
        report.EndPose.ShouldBe(new Pose(new Position(5, 2), Orientation.North));
    }

    [Fact]
    public void Then_An_Unscented_Coordinate_Still_Allows_Loss()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Left, RobotCommand.Forward, RobotCommand.Right, RobotCommand.Forward], _grid);

        // Assert
        report.IsLost.ShouldBeTrue();
    }

    [Fact]
    public void Then_Scents_Are_Not_Shared_With_Another_Grid()
    {
        // Act
        var report = _sut.Simulate([RobotCommand.Forward], new Grid(new Position(5, 3)));

        // Assert
        report.IsLost.ShouldBeTrue();
    }
}
