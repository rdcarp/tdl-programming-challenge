using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Robot;

public class When_A_Bot_Stays_Within_The_Grid
{
    private const int GridMaxX = 5;
    private const int GridMaxY = 3;

    [Theory]
    [InlineData(2, GridMaxY - 1, Orientation.North)]
    [InlineData(GridMaxX - 1, 2, Orientation.East)]
    [InlineData(2, 1, Orientation.South)]
    [InlineData(1, 2, Orientation.West)]
    public void Then_Reaching_The_Boundary_Does_Not_Lose_The_Robot(int x, int y, Orientation orientation)
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(x, y), orientation));
        var grid = new Grid(new Position(GridMaxX, GridMaxY));

        // Act
        var report = sut.Simulate([RobotCommand.Forward], grid);

        // Assert
        report.IsLost.ShouldBeFalse();
    }

    [Fact]
    public void Then_Turning_On_A_Boundary_Point_Does_Not_Lose_The_Robot()
    {
        // Arrange
        var sut = new Robot(new Pose(new Position(0, 0), Orientation.North));
        var grid = new Grid(new Position(0, 0));

        // Act
        var report = sut.Simulate([RobotCommand.Left, RobotCommand.Right], grid);

        // Assert
        report.IsLost.ShouldBeFalse();
    }
}
