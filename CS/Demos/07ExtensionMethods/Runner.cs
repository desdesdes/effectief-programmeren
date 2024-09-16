using Demos.Collecties;

namespace Demos.ExtensionMethods;

static class Runner
{
  public static void RunMe()
  {
    var p1 = new Person { Id = Guid.NewGuid(), FirstName = "Bill", LastName = "Gates" };
    var p2 = new Person { Id = Guid.NewGuid(), FirstName = "Steve", LastName = "Ballmer" };

    Console.WriteLine(GetFullName(p1));
  }

  static string GetFullName(Person p)
  {
    return $"{p.FirstName} {p.LastName}";
  }
}
