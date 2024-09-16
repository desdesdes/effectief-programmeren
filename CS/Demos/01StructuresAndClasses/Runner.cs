namespace Demos.StructuresAndClasses;

struct Point
{
  public int X;
  public int Y;
}

static class Runner
{
  public static void RunMe()
  {
    var p = new Point() { X = 3 };

    Move(p);

    Console.WriteLine($"X: {p.X}, Y: {p.Y}");
  }

  private static void Move(Point p)
  {
    p.X++;
    p.Y++;
  }
}
