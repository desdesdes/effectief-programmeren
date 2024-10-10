using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demos.DI;

internal class Runner
{
  public static async void RunMe()
  {
    var builder = Host.CreateApplicationBuilder();
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
    builder.Logging.AddConsole();
    builder.Services.AddSingleton<MyApp>();
    using IHost host = builder.Build();
    await host.StartAsync();

    try
    {
      //do the actual work here
      var bar = host.Services.GetRequiredService<MyApp>();
      bar.Runme();
    }
    finally
    {
      await host.StopAsync();
    }
  }
}
