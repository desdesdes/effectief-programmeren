using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Demos.Xml;

static class Runner
{
  private static DateTime _startTime;
  private static long _startSize;
  private const string _xmlFileName = "c:\\temp\\demo.xml";
  private const string _xmlFileName2 = "c:\\temp\\demo2.xml";
  private const int _times = 200000;

  public static void RunMe()
  {
    DocumentSpeedTest();
    //OptimizedReadSpeedTest();
    //BuildUsingXmlWriter();
    //BuildUsingXElement();
    //WriteTransformTest();
    //XmlSerializerTest();
  }

  private static void DocumentSpeedTest()
  {
    StoreStat();

    var document = new System.Xml.XmlDocument();
    document.Load(_xmlFileName);
    Console.WriteLine(document.DocumentElement!.ChildNodes.Count);

    //var document = XDocument.Load(_xmlFileName);
    //Console.WriteLine(document.Elements().Elements().Count());

    PrintStat();

    Console.WriteLine(document.DocumentElement.ChildNodes.Count);
    //Console.WriteLine(document.Elements().Elements().Count());
  }

  private static void OptimizedReadSpeedTest()
  {
    StoreStat();

    var query = StreamElementsFromFile(_xmlFileName);

    Console.WriteLine(query.Count());

    PrintStat();

    Console.WriteLine(query.Count());
  }

  private static void BuildUsingXmlWriter()
  {
    StoreStat();

    var settings = new System.Xml.XmlWriterSettings
    {
      Indent = true
    };

    using var writer = System.Xml.XmlWriter.Create(_xmlFileName, settings);
    writer.WriteStartDocument();
    writer.WriteStartElement("Persons");
    for(int i = 0; i <= _times - 1; i++) //ca 100mb
    {
      writer.WriteStartElement("Person");
      writer.WriteAttributeString("id", Guid.NewGuid().ToString());
      writer.WriteElementString("FirstName", $"Bill {i}");
      writer.WriteElementString("LastName", $"Clinton {i}");
      writer.WriteElementString("Number", i.ToString());
      writer.WriteElementString("Address", "W. 125th street");
      writer.WriteElementString("Place", "New York");
      writer.WriteElementString("State", "NY");
      writer.WriteElementString("Zip", "10027");
      writer.WriteEndElement();

      writer.WriteStartElement("Person");
      writer.WriteAttributeString("id", Guid.NewGuid().ToString());
      writer.WriteElementString("FirstName", $"Barack {i}");
      writer.WriteElementString("LastName", $"Obama {i}");
      writer.WriteElementString("Number", i.ToString());
      writer.WriteElementString("Address", "Pennsylvania Ave NW");
      writer.WriteElementString("Place", "Washington");
      writer.WriteElementString("State", "DC");
      writer.WriteElementString("Zip", "20500");
      writer.WriteEndElement();
    }

    PrintStat();
  }

  private static void BuildUsingXElement()
  {
    StoreStat();

    var newXML = new XElement("Persons", StreamElementsFromMemory());

    //PrintStat();

    newXML.Save(_xmlFileName);

    PrintStat();
  }

  private static void WriteTransformTest()
  {
    StoreStat();

    var newXML =
      new XStreamingElement("NewPersons",
             from element in StreamElementsFromFile(_xmlFileName)
             where element.Element("State")!.Value == "NY"
             select new XElement("NewPerson",
                      new XAttribute("FirstName", element.Element("FirstName")!.Value),
                               new XAttribute("LastName", element.Element("LastName")!.Value)));

    newXML.Save(_xmlFileName2);

    PrintStat();
  }

  private static IEnumerable<XElement> StreamElementsFromFile(string fileName)
  {
    using var reader = System.Xml.XmlReader.Create(fileName);
    reader.ReadStartElement("Persons");

    while(reader.Read())
    {
      if((reader.NodeType == System.Xml.XmlNodeType.Element) && (reader.Name == "Person"))
      {
        yield return (XElement)XElement.ReadFrom(reader);
      }
    }

    reader.Close();
  }

  private static IEnumerable<XElement> StreamElementsFromMemory()
  {
    for(int i = 0; i <= _times - 1; i++)
    {
      yield return new XElement("Person",
               new XAttribute("id", Guid.NewGuid()),
                      new XElement("FirstName", $"Bill {i}"),
                             new XElement("LastName", $"Clinton {i}"),
                                    new XElement("Number", i),
                                           new XElement("Address", "W. 125th street"),
                                                  new XElement("Place", "New York"),
                                                         new XElement("State", "NY"),
                                                                new XElement("Zip", "10027"));

      yield return new XElement("Person",
               new XAttribute("id", Guid.NewGuid().ToString()),
                      new XElement("FirstName", $"Barack {i}"),
                             new XElement("LastName", $"Obama {i}"),
                                    new XElement("Number", i),
                                           new XElement("Address", "Pennsylvania Ave NW"),
                                                  new XElement("Place", "Washington"),
                                                         new XElement("State", "DC"),
                                                                new XElement("Zip", "20500"));
    }
  }

  private static void PrintStat()
  {
    GC.Collect();
    GC.WaitForFullGCComplete();

    var time = DateTime.Now - _startTime;
    var size = Process.GetCurrentProcess().PrivateMemorySize64 - _startSize;

    Console.WriteLine($"It took {time.TotalMilliseconds} ms.");
    Console.WriteLine($"It took {size / 1024} KBytes.");
  }

  private static void StoreStat()
  {
    GC.Collect();
    GC.WaitForFullGCComplete();

    _startTime = DateTime.Now;
    _startSize = Process.GetCurrentProcess().PrivateMemorySize64;
  }

  private static void XmlSerializerTest()
  {
    var persons = new Persons
    {
      new Person
      {
        Id = Guid.NewGuid(),
        FirstName = "Bill",
        LastName = "Clinton"
      }
    };

    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Persons));
    using(var stream = File.Create(_xmlFileName2))
    {
      serializer.Serialize(stream, persons);
    }

    StoreStat();

    using(var stream = File.OpenRead(_xmlFileName))
    {
      var newPersons = (Persons)serializer.Deserialize(stream)!;
      Console.WriteLine(newPersons.Count);
    }

    PrintStat();
  }
}
