using Shouldly;
using TDL.MartianRobots.Application.Domain;
using TDL.MartianRobots.Cli.Extensions;

namespace TDL.MartianRobots.Cli.Tests.Given_A_SimulationReport;

public class When_ToDisplayString_Is_Called
{
    [Theory]
    [InlineData(false, "3 3 N")]
    [InlineData(true, "3 3 N LOST")]
    public void Then_Lost_Is_Appended_Only_For_A_Lost_Robot(bool isLost, string expected)
    {
        // Arrange
        var sut = new SimulationReport(new Pose(new Position(3, 3), Orientation.North), isLost);

        // Act
        var result = sut.ToDisplayString();

        // Assert
        result.ShouldBe(expected);
    }
}
