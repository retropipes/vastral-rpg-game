using Microsoft.AspNetCore.Components;

namespace VastralRPG.Game.Tests.Mocks;
public class MockNavigationManager : NavigationManager
{
    public MockNavigationManager()
    {
        this.Initialize("https://test.com/", "https://test.com/testlink/");
    }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        this.Uri = uri;
    }
}
