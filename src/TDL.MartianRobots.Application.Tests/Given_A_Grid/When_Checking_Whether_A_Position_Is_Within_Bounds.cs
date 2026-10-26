using Shouldly;
using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application.Tests.Given_A_Grid;

public class When_Checking_Whether_A_Position_Is_Within_Bounds
{
    private readonly Grid _sut = new(new Position(5, 3));

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(5, 3, true)]
    [InlineData(2, 1, true)]
    [InlineData(-1, 1, false)]
    [InlineData(6, 1, false)]
    [InlineData(1, -1, false)]
    [InlineData(1, 4, false)]
    public void Then_Only_Positions_Within_Inclusive_Bounds_Are_Contained(int x, int y, bool expected)
    {
        // Act
        var result = _sut.Contains(new Position(x, y));

        // Assert
        result.ShouldBe(expected);
    }
}
