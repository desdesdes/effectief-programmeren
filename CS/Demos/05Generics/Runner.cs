namespace Demos.Generics;

static class Runner
{
  public static void RunMe()
  {
    var set1 = new WebSettings
    {
      VDir = "c:\\temp\\test"
    };

    SettingsUtils.Save(set1, "c:\\temp\\demo2.dat");

    var set2 = (WebSettings)SettingsUtils.Load("c:\\temp\\demo2.dat");

    Console.WriteLine(set2.VDir);
  }
}
