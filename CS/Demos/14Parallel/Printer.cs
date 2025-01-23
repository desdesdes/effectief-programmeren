namespace Demos.Parallel;

internal class Printer
{
  public PrintStatus PrintStatus { get; set; }

  public async Task PrintAsync(string data)
  {
    PrintStatus = PrintStatus.Printing;

    Console.WriteLine($"Printer: Starting {data}");
    await Task.Delay(2000);
    Console.WriteLine($"Printer: Finished {data}");

    PrintStatus = PrintStatus.Done;
  }

  public void Reset()
  {
    PrintStatus = PrintStatus.ReadyToReceive;
  }
}
