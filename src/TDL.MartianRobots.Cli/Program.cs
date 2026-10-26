using TDL.MartianRobots.Application.Domain;
using TDL.MartianRobots.Cli.Extensions;


var startingPose = new Pose(new Position(1, 1), Orientation.East);

var robot = new Robot(startingPose);
var report = robot.Simulate(
[
    RobotCommand.Right, 
    RobotCommand.Forward,
    RobotCommand.Right,
    RobotCommand.Forward,
    RobotCommand.Right,
    RobotCommand.Forward,
    RobotCommand.Right,
    RobotCommand.Forward
]);

Console.WriteLine(report.EndPose.ToDisplayString());
