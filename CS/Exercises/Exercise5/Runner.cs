using System.Xml;
using System.Xml.Linq;

namespace Exercises.Exercise5;

class Runner
{
  private const string _connectionString = @"Server=.\profitsqldev;Database=demo;Trusted_Connection=true;TrustServerCertificate=True";
  private const string _xmlFileName = @"c:\temp\demo.xml";

  public static void RunMe()
  {
    var persons = new AntaDataBuffer<PersonRow>(row => row.Id.Value);

    var elements = StreamXml(_xmlFileName);

    foreach (var personElement in elements.Take(100))
    {
      var personRow = new PersonRow();
      personRow.Id.Value = new Guid(personElement.Attribute("id")!.Value);
      personRow.FirstName.Value = personElement.Element("FirstName")!.Value;
      personRow.LastName.Value = personElement.Element("LastName")!.Value;
      persons.Add(personRow);

      WriteToSql(personRow);
    }

    Console.WriteLine("Loaded {0} persons.", persons.Count);
    Console.ReadKey();
  }

  private static void WriteToSql(PersonRow personRow)
  {
  }

  private static IEnumerable<XElement> StreamXml(string uri)
  {
    using (var reader = XmlReader.Create(uri))
    {
      reader.ReadStartElement("Persons");

      while (reader.Read())
      {
        if (reader.NodeType == XmlNodeType.Element)
        {
          yield return (XElement.ReadFrom(reader) as XElement)!;
        }
      }

      reader.Close();
    }
  }
}
