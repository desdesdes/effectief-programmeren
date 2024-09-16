namespace Demos.DelegatesAndEvents;

class ConsoleClient
{
  public void Print()
  {
    Printer.QueuePrintJob("Hallo world!");

    Console.WriteLine("ConsoleClient: Print finished");

    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
  }
}
