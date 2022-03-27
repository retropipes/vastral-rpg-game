using VastralRPG.Game.Engine.Models;
using Xunit;

namespace VastralRPG.Game.Engine.Tests.Models;

public class LocationTests
{
    [Fact]
    public void CreateSimpleLocation()
    {
        // arrange

        // act
        var loc = new Location
        {
            XCoordinate = 2,
            YCoordinate = 1,
            Name = "MyPlace",
            Description = "Test description of my place.",
            ImageName = "/test/test-image.png"
        };

        // assert
        Assert.NotNull(loc);
        Assert.Equal("MyPlace", loc.Name);
        Assert.Equal("Test description of my place.", loc.Description);
        Assert.Equal("/test/test-image.png", loc.ImageName);
        Assert.Equal(2, loc.XCoordinate);
        Assert.Equal(1, loc.YCoordinate);
    }
}
