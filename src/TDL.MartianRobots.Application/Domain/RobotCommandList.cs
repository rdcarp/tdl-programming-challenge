namespace TDL.MartianRobots.Application.Domain;

public sealed record RobotCommandList(Pose StartingPose, IReadOnlyList<RobotCommand> Commands);
