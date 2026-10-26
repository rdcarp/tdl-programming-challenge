using Shouldly;
using TDL.MartianRobots.Application.Domain;
using TDL.MartianRobots.Cli.Extensions;

namespace TDL.MartianRobots.Cli.Tests.Given_A_Pose;

public class When_ToDisplayString_Is_Called
{
    private readonly Pose _sut;

    public When_ToDisplayString_Is_Called()
    {
        _sut = new Pose(new Position(1, 3), Orientation.North);
    }

    [Fact]
    public void Then_Position_And_Orientation_Are_Formatted_As_Space_Separated_Values()
    {
        // Act
        var result = _sut.ToDisplayString();

        // Assert
        result.ShouldBe("1 3 N");
    }

    [Theory]
    [InlineData(Orientation.North, "1 1 N")]
    [InlineData(Orientation.East, "1 1 E")]
    [InlineData(Orientation.South, "1 1 S")]
    [InlineData(Orientation.West, "1 1 W")]
    public void Then_Orientation_Is_Formatted_As_A_Compass_Letter(Orientation orientation, string expected)
    {
        // Arrange
        var sut = new Pose(new Position(1, 1), orientation);

        // Act
        var result = sut.ToDisplayString();

        // Assert
        result.ShouldBe(expected);
    }
}
