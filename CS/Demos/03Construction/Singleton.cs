namespace Demos.Construction;

class Singleton
{
  private static readonly Singleton _instance = new Singleton();

  private Singleton()
  {
    SingletonCounter = 2;
  }

  public static Singleton Current => _instance;

  public int SingletonCounter { get; set; }
}
