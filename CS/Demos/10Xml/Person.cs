using System.Xml.Serialization;

namespace Demos.Xml;

public class Person
{
  [XmlAttribute("id")]
  public Guid Id { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int Number { get; set; }
  public string? Address { get; set; }
  public string? Place { get; set; }
  public string? State { get; set; }
  public string? Zip { get; set; }
}
