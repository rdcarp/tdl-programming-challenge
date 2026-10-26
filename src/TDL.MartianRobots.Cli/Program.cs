using TDL.MartianRobots.Application.Domain;
using TDL.MartianRobots.Cli.Extensions;


var startingPose = new Pose(new Position(1, 1), Orientation.North);

var robot = new Robot(startingPose);
var report = robot.Simulate([RobotCommand.Forward, RobotCommand.Forward]);

Console.WriteLine(report.EndPose.ToDisplayString());
