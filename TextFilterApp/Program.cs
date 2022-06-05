using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Services.Interfaces;
using Application.Services;
using Application;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddTransient<ITextFilterService, TextFilterService>()
        .AddTransient<IAssemblyLoader, AssemblyLoader>()
        .AddTransient<TextFilter>())
    .Build();

var textFilter = host.Services.GetRequiredService<TextFilter>();
textFilter.ApplyFiltersToFile("Application.Resources.TextFile.txt");

await host.RunAsync();

