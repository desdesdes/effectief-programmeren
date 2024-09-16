namespace Demos.ClassesAndModules;

static class Runner
{
  public static void RunMe()
  {
    AddAndPrintValueWithModule();

    //var threads = new Thread[10];

    //for (var i = 0; i < threads.Length; i++)
    //{
    //  threads[i] = new Thread(AddAndPrintValueWithModule);
    //}

    //for (var i = 0; i < threads.Length; i++)
    //{
    //  threads[i].Start();
    //}
  }

  private static void AddAndPrintValueWithModule()
  {
    StaticVar.PrintCalls();
    StaticVar.PrintCalls();
    StaticVar.PrintCalls();
  }
}
