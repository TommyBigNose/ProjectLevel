using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectLevel;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IGame, Game>();
builder.Services.AddSingleton<IDataSource, LocalDataSource>();
builder.Services.AddScoped<CommandManager>();

await builder.Build().RunAsync();
