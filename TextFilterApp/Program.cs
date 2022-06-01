using TextFilterApp;
using TextFilterApp.Services;
using TextFilterApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddScoped<ITextFilterService, TextFilterService>())
    .Build();

await host.StartAsync();

var resourceName = "Application.Resources.TextFile.txt";
var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
if (stream != null)
{
    using (var sr = new StreamReader(stream))
    {
        var textFilter = new TextFilter(host.Services.GetRequiredService<ITextFilterService>());

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

await host.StopAsync();
