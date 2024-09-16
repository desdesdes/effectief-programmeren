namespace Demos.Construction;

class RefHolder
{
  private readonly int _number;

  public RefHolder(int number)
  {
    _number = number;
  }

  public RefHolder? Reference { get; set; }

  ~RefHolder()
  {
    Console.WriteLine($"Finalize: {_number}");
  }
}
