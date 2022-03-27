using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.ViewModels;
using Xunit;

namespace VastralRPG.Game.Engine.Tests.ViewModels;

public class GameSessionTests
{
    [Fact]
    public void CreateGameSession()
    {
        // arrange

        // act
        var vm = new GameSession();

        // assert
        Assert.NotNull(vm);
        Assert.NotNull(vm.CurrentPlayer);
        Assert.NotNull(vm.Movement);
        Assert.Equal("RetroPipes", vm.CurrentPlayer.Name);
        Assert.Equal("Fighter", vm.CurrentPlayer.CharacterClass);
        Assert.Equal(10, vm.CurrentPlayer.HitPoints);
        Assert.Equal(1000, vm.CurrentPlayer.Gold);
        Assert.Equal(0, vm.CurrentPlayer.ExperiencePoints);
        Assert.Equal(1, vm.CurrentPlayer.Level);

        Assert.Equal(0, vm.CurrentLocation.XCoordinate);
        Assert.Equal(-1, vm.CurrentLocation.YCoordinate);
        Assert.Equal("Home", vm.CurrentLocation.Name);
        Assert.Equal("This is your home.", vm.CurrentLocation.Description);
        Assert.Equal("/images/locations/Home.png", vm.CurrentLocation.ImageName);
    }

    [Fact]
    public void OnLocationChanged()
    {
        // arrange
        var vm = new GameSession();
        var loc = new Location
        {
            XCoordinate = 10,
            YCoordinate = 10,
            Name = "MockLoc"
        };

        // act
        vm.OnLocationChanged(loc);

        // assert
        Assert.NotNull(vm.CurrentLocation);
        Assert.Equal(loc, vm.CurrentLocation);
    }
}
