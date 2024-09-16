namespace Demos.ClassesAndModules;

class ClassVar
{
  private int _counter = 1;

  public void PrintCalls()
  {
    Console.WriteLine($"{Environment.CurrentManagedThreadId} has value: {_counter}");
    _counter++;
  }
}
