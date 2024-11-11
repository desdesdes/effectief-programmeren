using System.Diagnostics.Tracing;

namespace Demos.Exceptions;

[EventSource]
public class MyEventSource : EventSource
{
  public static MyEventSource Log { get; } = new();

  private MyEventSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat)
  {
  }

  [Event(1)]
  public void Startup() => WriteEvent(1);

  [Event(2)]
  public void OpenFileStart(string fileName) => WriteEvent(2, fileName);

  [Event(3)]
  public void OpenFileStop() => WriteEvent(3);

  [Event(4, Level = EventLevel.Informational)]
  public void DevTrace(string message, string exception) => WriteEvent(4, message, exception);
}
