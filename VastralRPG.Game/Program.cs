using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VastralRPG.Game;
using VastralRPG.Game.Engine.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// add app-specific/custom services and view models here...
builder.Services.AddSingleton<GameSession>();
var host = builder.Build();
// initialize app-specific/custom services and view models here...

await host.RunAsync().ConfigureAwait(false);