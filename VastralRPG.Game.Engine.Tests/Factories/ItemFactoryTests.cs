using VastralRPG.Game.Engine.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VastralRPG.Game.Engine.Tests.Factories;

public class ItemFactoryTests
{
    [Fact]
    public void CreateGameItem_WithValidItemTypeId()
    {
        // arrange

        // act
        var item = ItemFactory.CreateGameItem(1001);

        // assert
        Assert.NotNull(item);
        Assert.Equal(1001, item.ItemTypeID);
        Assert.Equal("Pointy Stick", item.Name);
        Assert.Equal(1, item.Price);
    }

    [Fact]
    public void CreateGameItem_WithInvalidItemTypeId()
    {
        // arrange

        // act
        var item = ItemFactory.CreateGameItem(1);

        // assert
        Assert.Null(item);
    }
}
