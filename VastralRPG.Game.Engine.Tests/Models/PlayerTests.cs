using VastralRPG.Game.Engine.Models;
using Xunit;

namespace VastralRPG.Game.Engine.Tests.Models;

public class PlayerTests
{
    [Fact]
    public void CreateSimplePlayer()
    {
        // arrange

        // act
        var p = new Player
        {
            Name = "Test",
            Level = 1,
            CurrentHitPoints = 10,
            MaximumHitPoints = 10
        };

        // assert
        Assert.NotNull(p);
        Assert.Equal("Test", p.Name);
        Assert.Equal(string.Empty, p.CharacterClass);
        Assert.Equal(1, p.Level);
        Assert.Equal(10, p.CurrentHitPoints);
        Assert.Equal(10, p.MaximumHitPoints);
        Assert.Equal(0, p.ExperiencePoints);
        Assert.Equal(0, p.Gold);
        Assert.Equal(0, p.Inventory.Items.Count);
    }
}
