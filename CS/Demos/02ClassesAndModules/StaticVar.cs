namespace Demos.ClassesAndModules;

static class StaticVar
{
  private static int _counter = 1;

  public static void PrintCalls()
  {
    Console.WriteLine($"{Environment.CurrentManagedThreadId} has value: {_counter}");
    _counter++;
  }
}
