namespace Demos.FunctionalProgramming;

class Person(Guid id, string firstName, string lastName) : IComparable<Person>
{
  public Guid Id { get; set; } = id;
  public string FirstName { get; set; } = firstName;
  public string LastName { get; set; } = lastName;

  public int CompareTo(Person? other)
  {
    return FirstName.CompareTo(other?.FirstName);
  }
}
