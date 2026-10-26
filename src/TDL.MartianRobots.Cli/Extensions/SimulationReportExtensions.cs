using TDL.MartianRobots.Application.Domain;

namespace TDL.MartianRobots.Cli.Extensions;

public static class SimulationReportExtensions
{
    public static string ToDisplayString(this SimulationReport report) =>
        report.EndPose.ToDisplayString() + (report.IsLost ? " LOST" : "");
}
