using TextFilterApp;
using TextFilterApp.Services;
using TextFilterApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddTransient<ITextFilterService, TextFilterService>()
        .AddTransient<TextFilter>())
    .Build();

ApplyFiltersToTextFile(host.Services, "Application.Resources.TextFile.txt");
await host.RunAsync();

static void ApplyFiltersToTextFile(IServiceProvider services, string resourceName)
{
    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
    if (stream != null)
    {
        using (var sr = new StreamReader(stream))
        {
            var textFilter = services.GetRequiredService<TextFilter>();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var filteredWords = textFilter.FilterText(line);

                foreach (var word in filteredWords)
                {
                    Console.WriteLine(word);
                }
            }
        }
    }
    else
    {
        Console.WriteLine($"Failed loading resource: {resourceName}");
    }
}

