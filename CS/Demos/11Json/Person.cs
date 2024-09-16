using System.Text.Json.Serialization;

namespace Demos.Json;

class Person
{
  public Guid Id { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int Number { get; set; }
  public string? Address { get; set; }
  public string? Place { get; set; }
  public string? State { get; set; }
  public string? Zip { get; set; }
  [JsonIgnore]
  public string? NonSerializedData { get; set; }
}
