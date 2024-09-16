namespace Demos.Collecties;

class PersonCollection
{
  private readonly List<Person> _person = [];

  public void Add(Person p)
  {
    _person.Add(p);
  }
}
