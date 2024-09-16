namespace Demos.DelegatesAndEvents;

static class Printer
{
  public static void QueuePrintJob(string data)
  {
    ThreadPool.QueueUserWorkItem(DoPrint, data);
  }

  private static void DoPrint(object? state)
  {
    Console.WriteLine("Printer: DoPrint started");
    Thread.Sleep(2000);
    Console.WriteLine($"Printer: Printing {state}");
    Console.WriteLine("Printer: DoPrint ready");
  }
}
