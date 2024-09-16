namespace Exercises.Exercise5;

class PersonRow
{
  public AntaField<Guid> Id { get; set; } = new AntaField<Guid>();
  public AntaField<string> FirstName { get; set; } = new AntaField<string>();
  public AntaField<string> LastName { get; set; } = new AntaField<string>();
}
