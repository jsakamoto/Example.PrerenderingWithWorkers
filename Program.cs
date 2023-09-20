using Example.PrerenderingWithWorkers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpawnDev.BlazorJS;
using SpawnDev.BlazorJS.WebWorkers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

// Register the BlazorJS.WebWorkers services only when this app is running in a Web browser.
// (The BlazorWasmPreRendering.Build will invoke only the ConfigureServices method to pre-render inside a build process.)
builder.Services.AddBlazorJSRuntime();
builder.Services.AddWebWorkerService();
builder.Services.AddSingleton<IMyWorker, MyWorker>();

// BlazorJS.WebWorkers
await builder.Build().BlazorJSRunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
}
