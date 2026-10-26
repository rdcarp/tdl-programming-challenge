using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Leaves_The_Grid
{
    private const int MaxXCoordinates = 5;
    private const int MaxYCoordinates = 3;
    private readonly Grid _grid = new(new  Position(MaxXCoordinates, MaxYCoordinates));

    [Theory]
    [InlineData(2, MaxYCoordinates, Orientation.North)]
    [InlineData(MaxXCoordinates, 2, Orientation.East)]
    [InlineData(2, 0, Orientation.South)]
    [InlineData(0, 2, Orientation.West)]
    public void Then_The_Robot_Is_Lost(int x, int y, Orientation orientation)
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(x, y), orientation));

        // Act
        var report = sut.Simulate([RobotCommand.Forward], _grid);

        // Assert
        report.IsLost.ShouldBeTrue();
    }

    [Fact]
    public void Then_The_Last_Valid_Pose_Is_Reported_And_Remaining_Commands_Are_Ignored()
    {
        // Arrange
        const Orientation botOrientation = Orientation.North;
        var sut = new Robot(new Pose(new Position(MaxXCoordinates, MaxYCoordinates), botOrientation));

        // Act
        var report = sut.Simulate(
            [RobotCommand.Forward, RobotCommand.Forward, RobotCommand.Forward], _grid);

        // Assert
        report.EndPose.ShouldBe(new Pose(new Position(MaxXCoordinates, MaxYCoordinates), botOrientation));
    }

    [Fact]
    public void Then_A_New_Simulation_Starts_From_The_Original_Pose()
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(3, 2), Orientation.North));
        sut.Simulate([RobotCommand.Forward, RobotCommand.Forward], _grid);

        // Act
        var report = sut.Simulate([], _grid);

        // Assert
        report.ShouldBe(new SimulationReport(new Pose(new Position(3, 2), Orientation.North), false));
    }
}
