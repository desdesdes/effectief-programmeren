namespace Demos.Collecties;

static class Runner
{
  public static void RunMe()
  {
    var persons = new PersonCollection();

    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Bill", LastName = "Gates" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Steve", LastName = "Ballmer" });

    //foreach (var item in persons)
    //{
    //  Console.WriteLine($"First: {item.FirstName}, Last {item.LastName}");
    //}

    // Console.WriteLine($"First: {persons[1].FirstName}, Last {persons[1].LastName}");
  }
}
