namespace Demos.Linq;

static class Runner
{
  public static void RunMe()
  {
    var persons = new List<Person>();

    LoadData(persons);

    var newPersons = persons;

    foreach(var person in newPersons)
    {
      Console.WriteLine($"{person.FirstName} {person.LastName}");
    }
  }

  private static void LoadData(List<Person> persons)
  {
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Brain", LastName = "Harry" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Beauchemin" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Craig", LastName = "Freedman" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Don", LastName = "Box" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Ian", LastName = "Griffith" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Jeff", LastName = "Beehler" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Joel", LastName = "Pobar" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Rob", LastName = "Mensching" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Robert", LastName = "McLaws" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Sara", LastName = "Ford" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Scott", LastName = "Guthrie" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Robert", LastName = "McLaws" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Brad", LastName = "Adams" });
    persons.Add(new Person { Id = Guid.NewGuid(), FirstName = "Chris", LastName = "Anderson" });
  }
}
