namespace TDL.MartianRobots.Application.Domain;

public sealed class Robot(Pose startingPose)
{
    public SimulationReport Simulate(IEnumerable<RobotCommand> commands, Grid grid)
    {
        var pose = startingPose;
        foreach (var command in commands)
        {
            var nextPose = command switch
            {
                RobotCommand.Forward => pose with { Position = MoveForward(pose) },
                RobotCommand.Left => pose with { Orientation = TurnLeft(pose.Orientation) },
                RobotCommand.Right => pose with { Orientation = TurnRight(pose.Orientation) },
                _ => pose
            };

            if (!grid.Contains(nextPose.Position))
            {
                return new SimulationReport(pose, true);
            }

            pose = nextPose;
        }

        return new SimulationReport(pose, false);
    }

    private static Position MoveForward(Pose pose) => pose.Orientation switch
    {
        Orientation.North => new Position(pose.Position.X, pose.Position.Y + 1),
        Orientation.East => new Position(pose.Position.X + 1, pose.Position.Y),
        Orientation.South => new Position(pose.Position.X, pose.Position.Y - 1),
        Orientation.West => new Position(pose.Position.X - 1, pose.Position.Y),
        _ => pose.Position
    };

    private static Orientation TurnLeft(Orientation orientation) => orientation switch
    {
        Orientation.North => Orientation.West,
        Orientation.West => Orientation.South,
        Orientation.South => Orientation.East,
        Orientation.East => Orientation.North,
        _ => orientation
    };

    private static Orientation TurnRight(Orientation orientation) => orientation switch
    {
        Orientation.North => Orientation.East,
        Orientation.East => Orientation.South,
        Orientation.South => Orientation.West,
        Orientation.West => Orientation.North,
        _ => orientation
    };
}
