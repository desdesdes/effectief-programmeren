namespace Exercises.Exercise3;

class PersonRow : AntaRow
{
  public AntaField<string> FirstName { get; set; } = new AntaField<string>();
  public AntaField<string> LastName { get; set; } = new AntaField<string>();
}
