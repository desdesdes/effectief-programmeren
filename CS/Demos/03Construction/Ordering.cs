namespace Demos.Construction;

class Ordering
{
  private string _b = Log("b");
  private string _a = Log("a");

  private static string _c = Log("c");
  private static string _d = Log("d");

  static Ordering()
  {
    Console.WriteLine("static constructor");
  }

  public Ordering()
  {
    Console.WriteLine("Constructor");
  }

  private static string Log(string data)
  {
    Console.WriteLine(data);
    return data;
  }

  public static void DoNothingStatic()
  {
    Console.WriteLine("DoNothingStatic");
  }

  public void DoNothingInstance()
  {
    Console.WriteLine("DoNothingInstance");
  }
}
