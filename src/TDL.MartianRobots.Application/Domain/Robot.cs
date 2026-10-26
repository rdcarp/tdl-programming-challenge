namespace TDL.MartianRobots.Application.Domain;

public sealed class Robot(Pose startingPose)
{
    public SimulationReport Simulate(IEnumerable<RobotCommand> commands)
    {
        var pose = startingPose;
        foreach (var command in commands)
        {
            pose = command switch
            {
                RobotCommand.Forward => pose with { Position = new Position(pose.Position.X, pose.Position.Y + 1) },
                _ => pose // TODO: Handle other commands
            };
        }

        return new SimulationReport(pose);
    }
}
