using Microsoft.Extensions.Logging;

namespace Demos.DI;

internal class MyApp
{
  private readonly ILogger? _logger;

  public MyApp(ILogger<MyApp>? logger = null)
  {
    _logger = logger;
  }

  public void Runme()
  {
    _logger?.LogInformation("MyApp StartAsync");
  }
}
