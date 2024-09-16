namespace Exercises.Exercise3;

class AntaField<T>
{
  public T? Value { get; set; }

  public string? SqlValue
  {
    get
    {
      var stringValue = Value as string;

      if(string.IsNullOrEmpty(stringValue))
      {
        return stringValue;
      }

      return stringValue.Replace("\'", "\'\'");
    }
  }
}
