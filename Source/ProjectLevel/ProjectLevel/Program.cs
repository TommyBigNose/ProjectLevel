using System.Timers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectLevel;
using ProjectLevel.Contracts.v1.Interfaces;
using ProjectLevel.Services.v1.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(_ => new System.Timers.Timer());

builder.Services.AddScoped<IMilitaryFactory, MilitaryFactory>();
builder.Services.AddScoped<IBattleReadyFactory, BattleReadyFactory>();
builder.Services.AddScoped<IDataSource, LocalDataSource>();

builder.Services.AddScoped<IEconomy, Economy>();
builder.Services.AddScoped<IMilitary>(_ => new Military());
builder.Services.AddScoped<IItemChest, ItemChest>();
builder.Services.AddScoped<ICivilization, Civilization>();

builder.Services.AddScoped<IGame, Game>();

builder.Services.AddScoped<CommandManager>();

await builder.Build().RunAsync();
