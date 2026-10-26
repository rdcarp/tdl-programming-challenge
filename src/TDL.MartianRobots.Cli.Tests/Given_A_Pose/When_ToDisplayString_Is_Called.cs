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
}
