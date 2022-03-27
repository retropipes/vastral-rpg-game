using Bunit;
using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Shared;
using VastralRPG.Game.Tests.Mocks;
using Xunit;

namespace VastralRPG.Game.Tests.Shared;

public class LocationComponentTests
{
    [Fact]
    public void SimpleRender_WithEmptyLocation()
    {
        // arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoriseServices();

        // act
        var cut = ctx.RenderComponent<LocationComponent>();

        // assert
        var expected =
@"  <div style=""border: 1px solid gainsboro; text-align: center"">
      <div></div>
      <figure class=""figure"">
        <img src="""" class=""figure-img img-fluid"">
        <figcaption class=""figure-caption"">
        </figcaption>
      </figure>
    </div>
";
        cut.MarkupMatches(expected);
    }

    [Fact]
    public void SimpleRender_WithLocation()
    {
        // arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoriseServices();

        var testLocation = new Location
        {
            Name = "TestLocation",
            Description = "Test description.",
            ImageName = "/test/test-image.png"
        };
        var parameter = ComponentParameterFactory.Parameter("Location", testLocation);

        // act
        var cut = ctx.RenderComponent<LocationComponent>(parameter);

        // assert
        var expected =
@"  <div style=""border: 1px solid gainsboro; text-align: center"">
      <div>TestLocation</div>
      <figure class=""figure"">
        <img src=""/test/test-image.png"" class=""figure-img img-fluid"">
        <figcaption class=""figure-caption"">
          Test description.
        </figcaption>
      </figure>
    </div>
";
        cut.MarkupMatches(expected);
    }
}
