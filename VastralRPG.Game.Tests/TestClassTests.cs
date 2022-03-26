using Xunit;

namespace VastralRPG.Game.Tests;

public class UnitTest1
{
    [Fact]
    public void DoSomething_Test()
    {
        // arrange
        var cut = new TestClass();
        // act
        var result = cut.DoSomething("TestAbc");
        // assert
        Assert.True(result);
        Assert.Equal("TestAbc", cut.Name);
    }
}