namespace TDL.MartianRobots.Application.Domain;

public sealed class Grid(Position upperRight)
{
    private readonly HashSet<GridTileEdge> _scents = [];

    internal bool HasScent(GridTileEdge edge) => _scents.Contains(edge);

    internal void LeaveScent(GridTileEdge edge) => _scents.Add(edge);

    public bool Contains(Position position) =>
        position.X >= 0 && position.X <= upperRight.X &&
        position.Y >= 0 && position.Y <= upperRight.Y;
}
