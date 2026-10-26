using Shouldly;
using TDL.MartianRobots.Application.Domain;
using static TDL.MartianRobots.Application.Domain.RobotCommand;

namespace TDL.MartianRobots.Application.Tests.Given_A_Simulation;

public class When_Running_The_Sample_Command_Lists
{
    private readonly Simulation _sut = new();
    private readonly RobotCommandList[] _commandLists =
    [
        new(new Pose(new Position(1, 1), Orientation.East),
            [Right, Forward, Right, Forward, Right, Forward, Right, Forward]),
        new(new Pose(new Position(3, 2), Orientation.North),
            [Forward, Right, Right, Forward, Left, Left, Forward, Forward, Right, Right, Forward, Left, Left]),
        new(new Pose(new Position(0, 3), Orientation.West),
            [Left, Left, Forward, Forward, Forward, Left, Forward, Left, Forward, Left])
    ];

    [Fact]
    public void Then_Reports_Are_Returned_In_Command_List_Order_With_Shared_Scents()
    {
        // Act
        var reports = _sut.Run(new Position(5, 3), _commandLists);

        // Assert
        reports.ShouldBe(new[]
        {
            new SimulationReport(new Pose(new Position(1, 1), Orientation.East), false),
            new SimulationReport(new Pose(new Position(3, 3), Orientation.North), true),
            new SimulationReport(new Pose(new Position(2, 3), Orientation.South), false)
        });
    }

    [Fact]
    public void Then_A_New_Run_Does_Not_Inherit_Previous_Scents()
    {
        // Arrange
        _sut.Run(new Position(5, 3), _commandLists);

        // Act
        var reports = _sut.Run(new Position(5, 3), _commandLists);

        // Assert
        reports[1].IsLost.ShouldBeTrue();
    }
}
