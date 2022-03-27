using Blazorise;
using Blazorise.Bootstrap5;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace VastralRPG.Game.Tests.Mocks;

public static class TestServiceProviderExtensions
{
    public static void AddBlazoriseServices(this TestServiceProvider services)
    {
        services.AddSingleton<IJSRuntime>(new Mock<IJSRuntime>().Object);
        services.AddSingleton<IIconProvider>(new Mock<IIconProvider>().Object);
        services.AddBlazorise(options =>
        {
            options.Immediate = true;
        })
        .AddBootstrap5Providers();
    }
}
