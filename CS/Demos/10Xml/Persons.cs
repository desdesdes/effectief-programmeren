using System.Xml.Serialization;

namespace Demos.Xml;

[XmlRoot("Persons")]
public class Persons : List<Person>
{
}
