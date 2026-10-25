using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Cli.Extensions;

public static class PoseExtensions
{
    public static string ToDisplayString(this Pose pose) =>
        $"{pose.Position.X} {pose.Position.Y} {pose.Orientation.ToString()[0]}";
}
