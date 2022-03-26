using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using VastralRPG.Game.Shared;
using VastralRPG.Game.Tests.Mocks;
using Xunit;

namespace VastralRPG.Game.Tests.Shared;

public class NavMenuTests
{
    [Fact]
    public void SimpleRender()
    {
        // arrange
        using var ctx = new TestContext();
        ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager());
        // act
        var cut = ctx.RenderComponent<NavMenu>();
        // assert
        var expected = @"<span class=""oi oi-home"" aria-hidden=""true""";
        Assert.Contains(expected, cut.Markup);
        expected = @"<div class=""collapse"" blazor:onclick=""2""";
        Assert.Contains(expected, cut.Markup);
    }
}
