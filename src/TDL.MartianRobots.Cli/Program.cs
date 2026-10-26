using TDL.MartianRobots.Application.Domain;
using TDL.MartianRobots.Cli.Extensions;

var grid = new Grid(new Position(5, 3));
var startingPose = new Pose(new Position(3, 2), Orientation.North);
var robot = new Robot(startingPose);
var report = robot.Simulate(
[
    RobotCommand.Forward,
    RobotCommand.Right,
    RobotCommand.Right,
    RobotCommand.Forward,
    RobotCommand.Left,
    RobotCommand.Left,
    RobotCommand.Forward,
    RobotCommand.Forward,
    RobotCommand.Right,
    RobotCommand.Right,
    RobotCommand.Forward,
    RobotCommand.Left,
    RobotCommand.Left
], grid);

Console.WriteLine(report.ToDisplayString());
