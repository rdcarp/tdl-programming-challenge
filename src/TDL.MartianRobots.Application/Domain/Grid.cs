namespace TDL.MartianRobots.Application.Domain;

public sealed class Grid(Position upperRight)
{
    public bool Contains(Position position) =>
        position.X >= 0 && position.X <= upperRight.X &&
        position.Y >= 0 && position.Y <= upperRight.Y;
}
