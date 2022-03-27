using Microsoft.AspNetCore.Components;
using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;
namespace VastralRPG.Game.Engine.Tests.ViewModels;
public class MovementUnitTests
{
    private static readonly IList<Location> locations = new List<Location>
        {
            new Location
            {
                XCoordinate = 0,
                YCoordinate = -1,
                Name = "Home",
            },
            new Location
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Name = "Town Square",
            },
            new Location
            {
                XCoordinate = -1,
                YCoordinate = -1,
                Name = "Left",
            },
            new Location
            {
                XCoordinate = 1,
                YCoordinate = -1,
                Name = "Right",
            },
            new Location
            {
                XCoordinate = 0,
                YCoordinate = -2,
                Name = "SouthEnd",
            }
        };
    private readonly World testWorld = new World(locations);
    private readonly EventCallbackFactory eventFactory = new EventCallbackFactory();
    [Fact]
    public void CreateMovementUnit()
    {
        // arrange
        // act
        var move = new MovementUnit(testWorld);
        // assert
        Assert.NotNull(move.CurrentLocation);
        Assert.Equal(0, move.CurrentLocation.XCoordinate);
        Assert.Equal(-1, move.CurrentLocation.YCoordinate);
        Assert.Equal("Home", move.CurrentLocation.Name);
    }
    [Fact]
    public void CanMoveNorth()
    {
        // arrange
        var move = new MovementUnit(testWorld);
        // act
        var b = move.CanMoveNorth;
        // assert
        Assert.True(b);
    }
    [Fact]
    public void CanMoveEast()
    {
        // arrange
        var move = new MovementUnit(testWorld);
        // act
        var b = move.CanMoveEast;
        // assert
        Assert.True(b);
    }
    [Fact]
    public void CanMoveSouth()
    {
        // arrange
        var move = new MovementUnit(testWorld);
        // act
        var b = move.CanMoveSouth;
        // assert
        Assert.True(b);
    }
    [Fact]
    public void CanMoveWest()
    {
        // arrange
        var move = new MovementUnit(testWorld);
        // act
        var b = move.CanMoveWest;
        // assert
        Assert.True(b);
    }
    [Fact]
    public void MoveNorth()
    {
        // arrange
        var w = new World(locations);
        var move = new MovementUnit(w);
        Location newLocation = null;
        move.LocationChanged = eventFactory.Create<Location>(this, e => newLocation = e);
        // act
        move.MoveNorth();
        // assert
        Assert.NotNull(newLocation);
        Assert.Equal(0, newLocation.XCoordinate);
        Assert.Equal(0, newLocation.YCoordinate);
        Assert.Equal("Town Square", newLocation.Name);
    }
    [Fact]
    public void MoveEast()
    {
        // arrange
        var w = new World(locations);
        var move = new MovementUnit(w);
        Location newLocation = null;
        move.LocationChanged = eventFactory.Create<Location>(this, e => newLocation = e);
        // act
        move.MoveEast();
        // assert
        Assert.NotNull(newLocation);
        Assert.Equal(1, newLocation.XCoordinate);
        Assert.Equal(-1, newLocation.YCoordinate);
        Assert.Equal("Right", newLocation.Name);
    }
    [Fact]
    public void MoveSouth()
    {
        // arrange
        var w = new World(locations);
        var move = new MovementUnit(w);
        Location newLocation = null;
        move.LocationChanged = eventFactory.Create<Location>(this, e => newLocation = e);
        // act
        move.MoveSouth();
        // assert
        Assert.NotNull(newLocation);
        Assert.Equal(0, newLocation.XCoordinate);
        Assert.Equal(-2, newLocation.YCoordinate);
        Assert.Equal("SouthEnd", newLocation.Name);
    }
    [Fact]
    public void MoveWest()
    {
        // arrange
        var w = new World(locations);
        var move = new MovementUnit(w);
        Location newLocation = null;
        move.LocationChanged = eventFactory.Create<Location>(this, e => newLocation = e);
        // act
        move.MoveWest();
        // assert
        Assert.NotNull(newLocation);
        Assert.Equal(-1, newLocation.XCoordinate);
        Assert.Equal(-1, newLocation.YCoordinate);
        Assert.Equal("Left", newLocation.Name);
    }
    [Fact]
    public void Constructor_WithNullWorld()
    {
        Assert.Throws<ArgumentNullException>(() => new MovementUnit(null));
    }
}
