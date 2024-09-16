namespace Demos.Construction;

static class Runner
{
  public static void RunMe()
  {
    StartSettings();
    //StartOrdering();
    //StartFinalize();
    //StartSingleton();
  }

  private static void StartSettings()
  {
    var settings = new Settings();
    settings.DcomServer = "pwbvr";

    settings.Save("c:\\temp\\demo.dat");

    settings = new Settings("c:\\temp\\demo.dat");

    Console.WriteLine(settings.DcomServer);
  }

  private static void StartOrdering()
  {
    Console.WriteLine("Startup");
    Ordering.DoNothingStatic();
    Console.WriteLine("Ready");
  }

  private static void StartFinalize()
  {
    var x = new RefHolder(1);
    var y = new RefHolder(2);
    x.Reference = y;
    y.Reference = x;
  }

  private static void StartSingleton()
  {
    IncAndPrintSingleton(null);
  }

  private static void IncAndPrintSingleton(object? stateInfo)
  {
    var value = Singleton.Current.SingletonCounter;
    Console.WriteLine(value);
    Singleton.Current.SingletonCounter = value + 1;
  }
}
