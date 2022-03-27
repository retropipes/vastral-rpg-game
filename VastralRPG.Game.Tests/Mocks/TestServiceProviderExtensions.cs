using Blazorise;
using Blazorise.Bootstrap5;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace VastralRPG.Game.Tests.Mocks;

public static class TestServiceProviderExtensions
{
    public static void AddBlazoriseServices(this TestServiceProvider services)
    {
        services.AddBlazorise(options =>
        {
            options.Immediate = true;
        })
        .AddBootstrap5Providers();
    }

    class MockJSRuntime : IJSRuntime
    {
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            return new ValueTask<TValue>();
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            return new ValueTask<TValue>();
        }
    }

    class MockIconProvider : IIconProvider
    {
        public bool IconNameAsContent => false;

        public string GetIconName(IconName name)
        {
            return string.Empty;
        }

        public string GetIconName(string customName)
        {
            return string.Empty;
        }

        public string GetIconName(IconName name, IconStyle iconStyle)
        {
            throw new System.NotImplementedException();
        }

        public string Icon(object name, IconStyle iconStyle)
        {
            return string.Empty;
        }

        public string IconSize(IconSize iconSize)
        {
            return string.Empty;
        }

        public void SetIconName(IconName name, string newName)
        {
        }
    }
}
