using System.Diagnostics;
using System.Xml.Linq;

namespace Exercises.Exercise4;

class Runner
{
  private static DateTime _startTime;
  private static long _startSize;
  private const string XmlFileName = @"c:\temp\demo.xml";

  public static void RunMe()
  {
    var persons = new AntaDataBuffer<PersonRow>(row => row.Id.Value);

    StoreStat();

    var elements = XElement.Load(XmlFileName).Elements();

    // Zet de bovenstaande code regel onder commentaar en haal het commentaar bij de
    // onderstaande regel weg. Implementeer de StreamXml functie
    // var elements = StreamXml(XmlFileName);

    PrintStat();

    foreach(var personElement in elements)
    {
      var personRow = new PersonRow();
      personRow.Id.Value = new Guid(personElement.Attribute("id")!.Value);
      personRow.FirstName.Value = personElement.Element("FirstName")!.Value;
      personRow.LastName.Value = personElement.Element("LastName")!.Value;
      persons.Add(personRow);
    }

    PrintStat();
    Console.WriteLine("Loaded {0} persons.", persons.Count);
    Console.ReadKey();

    Console.WriteLine("List:");
    foreach(var person in persons)
    {
      Console.WriteLine("{0} {1}", person.FirstName.Value, person.LastName.Value);
    }
  }

  private static void StoreStat()
  {
    GC.Collect();
    GC.WaitForFullGCComplete();

    _startTime = DateTime.Now;
    _startSize = Process.GetCurrentProcess().PrivateMemorySize64;
  }

  private static void PrintStat()
  {
    GC.Collect();
    GC.WaitForFullGCComplete();

    var time = DateTime.Now - _startTime;
    var size = Process.GetCurrentProcess().PrivateMemorySize64 - _startSize;

    Console.WriteLine("It took {0} ms.", time.TotalMilliseconds);
    Console.WriteLine("It took {0} KBytes.", size / 1024);
  }

  private static IEnumerable<XElement> StreamXml(string uri)
  {
    return [];
  }
}
