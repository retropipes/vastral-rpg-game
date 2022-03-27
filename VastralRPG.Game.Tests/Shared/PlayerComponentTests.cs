using Bunit;
using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Shared;
using VastralRPG.Game.Tests.Mocks;
using Xunit;

namespace VastralRPG.Game.Tests.Shared;

public class PlayerComponentTests
{
    [Fact]
    public void SimpleRender_WithEmptyPlayer()
    {
        // arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoriseServices();

        // act
        var cut = ctx.RenderComponent<PlayerComponent>();

        // assert
        var expected =
@"    <table class=""b-table table table-sm table-borderless"">
      <thead>
        <tr>
          <th scope=""col""  rowspan=""2"">
            Player Data
          </th>
        </tr>
      </thead>
      <tbody>
        <tr >
          <td >
            Name:
          </td>
          <td >
          </td>
        </tr>
        <tr >
          <td >
            Class:
          </td>
          <td >
          </td>
        </tr>
        <tr >
          <td >
            Hit points:
          </td>
          <td >
            0
          </td>
        </tr>
        <tr >
          <td >
            Gold:
          </td>
          <td >
            0
          </td>
        </tr>
        <tr >
          <td >
            XP:
          </td>
          <td >
            0
          </td>
        </tr>
        <tr >
          <td >
            Level:
          </td>
          <td >
            0
          </td>
        </tr>
      </tbody>
    </table>
";
        cut.MarkupMatches(expected);
    }

    [Fact]
    public void SimpleRender_WithPlayer()
    {
        // arrange
        using var ctx = new TestContext();
        ctx.Services.AddBlazoriseServices();

        var testPlayer = new Player
        {
            Name = "TestPlayer",
            CharacterClass = "TestClass",
            CurrentHitPoints = 8,
            Gold = 10,
            ExperiencePoints = 101,
            Level = 1,
        };
        var parameter = ComponentParameterFactory.Parameter("Player", testPlayer);

        // act
        var cut = ctx.RenderComponent<PlayerComponent>(parameter);

        // assert
        var expected =
@"    <table class=""b-table table table-sm table-borderless"">
      <thead>
        <tr>
          <th scope=""col"" rowspan=""2"">
            Player Data
          </th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>
            Name:
          </td>
          <td>
            TestPlayer
          </td>
        </tr>
        <tr>
          <td>
            Class:
          </td>
          <td>
            TestClass
          </td>
        </tr>
        <tr>
          <td>
            Hit points:
          </td>
          <td>
            8
          </td>
        </tr>
        <tr>
          <td>
            Gold:
          </td>
          <td>
            10
          </td>
        </tr>
        <tr>
          <td>
            XP:
          </td>
          <td>
            101
          </td>
        </tr>
        <tr>
          <td>
            Level:
          </td>
          <td>
            1
          </td>
        </tr>
      </tbody>
    </table>
";
        cut.MarkupMatches(expected);
    }
}