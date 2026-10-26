using TDL.MartianRobots.Application;
using TDL.MartianRobots.Application.Domain;
using TDL.MartianRobots.Cli.Extensions;

using static TDL.MartianRobots.Application.Domain.RobotCommand;

RobotCommandList robot1 = new(new Pose(new Position(1, 1), Orientation.East),
    [Right, Forward, Right, Forward, Right, Forward, Right, Forward]);

RobotCommandList robot2 = new(new Pose(new Position(3, 2), Orientation.North),
    [Forward, Right, Right, Forward, Left, Left, Forward, Forward, Right, Right, Forward, Left, Left]);

RobotCommandList robot3 = new(new Pose(new Position(0, 3), Orientation.West),
    [Left, Left, Forward, Forward, Forward, Left, Forward, Left, Forward, Left]);

RobotCommandList[] commandLists = [robot1, robot2, robot3];

var simulation = new Simulation();
var reports = simulation.Run(new Position(5, 3), commandLists);

foreach (var report in reports)
{
    Console.WriteLine(report.ToDisplayString());
}
