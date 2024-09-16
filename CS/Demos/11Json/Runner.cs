using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Demos.Json;

static class Runner
{
  private static DateTime _startTime;
  private static long _startSize;
  private const string _jsonFileName = "c:\\temp\\demo.json";
  private const string _jsonFileName2 = "c:\\temp\\demo2.json";
  private const int _times = 200000;

  public static void RunMe()
  {
    DocumentSpeedTest();
    //OptimizedReadSpeedTest();
    //BuildUsingUtf8JsonWriter();
    //BuildUsingJsonElement();
    //JsonSerializerTest();
  }

  private static void DocumentSpeedTest()
  {
    StoreStat();

    var document = JsonDocument.Parse(File.ReadAllBytes(_jsonFileName));
    Console.WriteLine(document.RootElement.GetArrayLength());

    PrintStat();

    Console.WriteLine(document.RootElement.GetArrayLength());
  }

  private static void OptimizedReadSpeedTest()
  {
    StoreStat();

    var result = ReadFromFileAsync(_jsonFileName).Result;

    Console.WriteLine(result);

    PrintStat();

    Console.WriteLine(result);
  }

  private static void BuildUsingUtf8JsonWriter()
  {
    StoreStat();

    var settings = new JsonWriterOptions
    {
      Indented = true
    };

    using var stream = File.OpenWrite(_jsonFileName);
    using var writer = new Utf8JsonWriter(stream, settings);
    writer.WriteStartArray();

    for (int i = 0; i < _times; i++) //ca 100mb
    {
      writer.WriteStartObject();
      writer.WriteString("Id", Guid.NewGuid().ToString());
      writer.WriteString("FirstName", $"Bill {i}");
      writer.WriteString("LastName", $"Clinton {i}");
      writer.WriteNumber("Number", i);
      writer.WriteString("Address", "W. 125th street");
      writer.WriteString("Place", "New York");
      writer.WriteString("State", "NY");
      writer.WriteString("Zip", "10027");
      writer.WriteEndObject();

      writer.WriteStartObject();
      writer.WriteString("Id", Guid.NewGuid().ToString());
      writer.WriteString("FirstName", $"Barack {i}");
      writer.WriteString("LastName", $"Obama {i}");
      writer.WriteNumber("Number", i);
      writer.WriteString("Address", "Pennsylvania Ave NW");
      writer.WriteString("Place", "Washington");
      writer.WriteString("State", "DC");
      writer.WriteString("Zip", "20500");
      writer.WriteEndObject();

      if (i % 1000 == 0)
      {
        writer.Flush(); // flushes from memory to disk every 1000 elements, can be tuned to optimize the performance
      }
    }

    writer.WriteEndArray();

    PrintStat();
  }

  private static void BuildUsingJsonElement()
  {
    StoreStat();

    var settings = new JsonWriterOptions
    {
      Indented = true
    };

    using var stream = File.OpenWrite(_jsonFileName2);
    using var writer = new Utf8JsonWriter(stream, settings);
    writer.WriteStartArray();

    int i = 0;
    foreach (var element in StreamElementsFromMemory())
    {
      i++;
      element.WriteTo(writer);

      if (i % 1000 == 0)
      {
        writer.Flush(); // flushes from memory to disk every 1000 elements, can be tuned to optimize the performance
      }
    }
    writer.WriteEndArray();

    PrintStat();
  }

  private static async Task<int> ReadFromFileAsync(string fileName)
  {
    using var stream = File.OpenRead(_jsonFileName);
    var options = new JsonSerializerOptions();
    var iterator = JsonSerializer.DeserializeAsyncEnumerable<JsonNode>(stream, options);

    int counter = 0;
    await foreach (var node in iterator)
    {
      counter++;
    }

    return counter;
  }

  private static IEnumerable<JsonObject> StreamElementsFromMemory()
  {
    for (int i = 0; i < _times; i++)
    {
      yield return new JsonObject
      {
        ["id"] = Guid.NewGuid(),
        ["FirstName"] = $"Bill {i}",
        ["LastName"] = $"Clinton {i}",
        ["Number"] = i,
        ["Address"] = "W. 125th street",
        ["Place"] = "New York",
        ["State"] = "NY",
        ["Zip"] = "10027"
      };

      yield return new JsonObject
      {
        ["id"] = Guid.NewGuid(),
        ["FirstName"] = $"Barack {i}",
        ["LastName"] = $"Obama {i}",
        ["Number"] = i,
        ["Address"] = "Pennsylvania Ave NW",
        ["Place"] = "Washington",
        ["State"] = "DC",
        ["Zip"] = "20500"
      };
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

  private static void JsonSerializerTest()
  {
    var persons = new Persons
    {
      new Person
      {
        Id = Guid.NewGuid(),
        FirstName = "Bill",
        LastName = "Clinton",
        NonSerializedData = "You cannot see this"
      }
    };

    using(var stream = File.Create(_jsonFileName2))
    {
      JsonSerializer.Serialize(stream, persons, MyJsonSerializerContext.Default.Persons);
    }

    StoreStat();

    using(var stream = File.OpenRead(_jsonFileName))
    {
      var newPersons = JsonSerializer.Deserialize(stream, MyJsonSerializerContext.Default.Persons)!;
      Console.WriteLine(newPersons.Count);
    }

    PrintStat();
  }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Persons))]
internal partial class MyJsonSerializerContext : JsonSerializerContext
{
}
