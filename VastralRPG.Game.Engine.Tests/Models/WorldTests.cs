using VastralRPG.Game.Engine.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace VastralRPG.Game.Engine.Tests.Models;

public class WorldTests
{
    private readonly IList<Location> _locations = new List<Location>
        {
            new Location
            {
                XCoordinate = 0,
                YCoordinate = -1,
                Name = "Home",
                Description = "My home",
                ImageName = "/images/locations/home.png"
            },
            new Location
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Name = "Town Square",
                Description = "the town square",
                ImageName = "/images/locations/town-square.png"
            }
        };

    [Fact]
    public void CreateSimpleWorld()
    {
        // arrange
        // act
        var world = new World(_locations);
        // assert
        Assert.NotNull(world);
    }

    [Fact]
    public void GetHomeLocation()
    {
        // arrange
        var world = new World(_locations);
        // act
        var loc = world.GetHomeLocation();
        // assert
        Assert.NotNull(loc);
        Assert.Equal(0, loc.XCoordinate);
        Assert.Equal(-1, loc.YCoordinate);
        Assert.Equal("Home", loc.Name);
    }

    [Fact]
    public void LocationAt_ValidLocation()
    {
        // arrange
        var world = new World(_locations);

        // act
        var loc = world.LocationAt(0, 0);

        // assert
        Assert.NotNull(loc);
        Assert.Equal(0, loc.XCoordinate);
        Assert.Equal(0, loc.YCoordinate);
        Assert.Equal("Town Square", loc.Name);
    }

    [Fact]
    public void LocationAt_InvalidLocation()
    {
        // arrange
        var world = new World(_locations);
        // act
        Assert.Throws<ArgumentOutOfRangeException>(() => world.LocationAt(-100, 100));
        // assert
    }

    [Fact]
    public void Constructor_WithNullLocations()
    {
        // arrange
        // act
        var w = new World(null);
        // assert
        Assert.False(w.HasLocationAt(0, 0));
    }

    [Fact]
    public void HasLocationAt_ValidLocation()
    {
        // arrange
        var world = new World(_locations);
        // act
        var b = world.HasLocationAt(0, 0);
        // assert
        Assert.True(b);
    }

    [Fact]
    public void HasLocationAt_InvalidLocation()
    {
        // arrange
        var world = new World(_locations);
        // act
        var b = world.HasLocationAt(-100, 100);
        // assert
        Assert.False(b);
    }
}
