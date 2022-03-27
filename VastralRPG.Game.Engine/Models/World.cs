namespace VastralRPG.Game.Engine.Models;

public class World
{
    private readonly IList<Location> locations;

    public World(IEnumerable<Location> locs)
    {
        this.locations = locs is null ? new List<Location>() : locs.ToList();
    }

    public Location LocationAt(int xCoordinate, int yCoordinate)
    {
        var loc = locations.FirstOrDefault(p => p.XCoordinate == xCoordinate && p.YCoordinate == yCoordinate);
        return loc ?? throw new ArgumentOutOfRangeException("Coordinates", "Provided coordinates could not be found in game world.");
    }

    public Location GetHomeLocation()
    {
        return this.LocationAt(0, -1);
    }
}
