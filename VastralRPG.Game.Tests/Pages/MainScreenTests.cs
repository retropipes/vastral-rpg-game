using Bunit;
using Microsoft.Extensions.DependencyInjection;
using VastralRPG.Game.Engine.ViewModels;
using VastralRPG.Game.Pages;
using VastralRPG.Game.Tests.Mocks;
using Xunit;

namespace VastralRPG.Game.Tests.Pages;

public class MainScreenTests
{
    private readonly GameSession session = new MockGameSession();

    [Fact]
    public void SimpleRender()
    {
        // arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoriseServices();
        ctx.Services.AddSingleton<GameSession>(session);
        var mod = ctx.JSInterop.SetupModule("./_content/Blazorise/button.js?v=1.0.1.0");
        mod.SetupVoid("initialize", _ => true);
        // act
        var cut = ctx.RenderComponent<MainScreen>();
        // assert
        var expected = @"<th scope=""col"" blazor:onclick=""24"" rowspan=""2"" blazor:ondrag=""25"" blazor:ondragend=""26"" blazor:ondragenter=""27"" blazor:ondragleave=""28"" blazor:ondragover=""29"" blazor:ondragstart=""30"" blazor:ondrop=""31""";
        Assert.Contains(expected, cut.Markup);
        Assert.Contains("Player Data", cut.Markup);
        Assert.Contains("TestPlayer", cut.Markup);
        Assert.Contains("TestClass", cut.Markup);
    }
}
