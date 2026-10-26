using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Application;

public sealed class Simulation
{
    public IReadOnlyList<SimulationReport> Run(Position upperRight, IEnumerable<RobotCommandList> commandLists)
    {
        var grid = new Grid(upperRight);
        var reports = new List<SimulationReport>();

        foreach (var commandList in commandLists)
        {
            var robot = new Robot(commandList.StartingPose);
            reports.Add(robot.Simulate(commandList.Commands, grid));
        }

        return reports.AsReadOnly();
    }
}
