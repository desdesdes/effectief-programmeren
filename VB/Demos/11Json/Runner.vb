Imports System.IO
Imports System.Text.Json
Imports System.Text.Json.Nodes

Namespace Json
  Class Runner
    Private Shared _startTime As Date
    Private Shared _startSize As Long
    Private Const JsonFileName = "c:\temp\demo.json"
    Private Const JsonFileName2 = "c:\temp\demo2.json"

    Private Const Times As Integer = 200000

    Public Shared Sub RunMe()
      DocumentSpeedTestNewtonsoft()
      'DocumentSpeedTest()
      'OptimizedReadSpeedTest
      'BuildUsingUtf8JsonWriter()
      'BuildUsingJsonElement()
      'JsonSerializerTest()
    End Sub

    Private Shared Sub DocumentSpeedTest()
      StoreStat()

      Dim document = JsonDocument.Parse(File.ReadAllBytes(JsonFileName))
      Console.WriteLine(document.RootElement.GetArrayLength)

      PrintStat()

      Console.WriteLine(document.RootElement.GetArrayLength)
    End Sub

    Private Shared Sub DocumentSpeedTestNewtonsoft()
      StoreStat()

      Dim document = Newtonsoft.Json.Linq.JArray.Parse(File.ReadAllText(JsonFileName))
      Console.WriteLine(document.Count)

      PrintStat()

      Console.WriteLine(document.Count)
    End Sub

    Private Shared Sub OptimizedReadSpeedTest()
      StoreStat()

      Dim result = ReadFromFile(JsonFileName)
      'Dim result = ReadFromFileAsync(JsonFileName).Result

      Console.WriteLine(result)

      PrintStat()

      Console.WriteLine(result)
    End Sub

    Private Shared Sub BuildUsingUtf8JsonWriter()
      StoreStat()

      Dim settings = New JsonWriterOptions With {
        .Indented = True
      }

      Using stream = File.OpenWrite(JsonFileName)
        Using writer = New Utf8JsonWriter(stream, settings)
          writer.WriteStartArray()

          For i As Integer = 0 To Times - 1 'ca 100mb
            writer.WriteStartObject()
            writer.WriteString("Id", Guid.NewGuid().ToString())
            writer.WriteString("FirstName", $"Bill {i}")
            writer.WriteString("LastName", $"Clinton {i}")
            writer.WriteNumber("Number", i)
            writer.WriteString("Address", "W. 125th street")
            writer.WriteString("Place", "New York")
            writer.WriteString("State", "NY")
            writer.WriteString("Zip", "10027")
            writer.WriteEndObject()

            writer.WriteStartObject()
            writer.WriteString("Id", Guid.NewGuid().ToString())
            writer.WriteString("FirstName", $"Barack {i}")
            writer.WriteString("LastName", $"Obama {i}")
            writer.WriteNumber("Number", i)
            writer.WriteString("Address", "Pennsylvania Ave NW")
            writer.WriteString("Place", "Washington")
            writer.WriteString("State", "DC")
            writer.WriteString("Zip", "20500")
            writer.WriteEndObject()

            If i Mod 1000 = 0 Then
              writer.Flush() ' flushes from memory to disk every 1000 elements, can be tuned to optimize the performance
            End If
          Next

          writer.WriteEndArray()
        End Using

      End Using

      PrintStat()
    End Sub

    Private Shared Sub BuildUsingJsonElement()
      StoreStat()

      Dim settings = New JsonWriterOptions With {
        .Indented = True
      }

      Using stream = File.OpenWrite(JsonFileName2)
        Using writer = New Utf8JsonWriter(stream, settings)
          writer.WriteStartArray()

          Dim i As Integer = 0
          For Each element In StreamElementsFromMemory()
            i += 1
            element.WriteTo(writer)

            If i Mod 1000 = 0 Then
              writer.Flush() ' flushes from memory to disk every 1000 elements, can be tuned to optimize the performance
            End If
          Next
          writer.WriteEndArray()
        End Using
      End Using

      PrintStat()
    End Sub

    Private Shared Async Function ReadFromFileAsync(fileName As String) As Task(Of Integer)
      Using stream = File.OpenRead(JsonFileName)
        Dim options = New JsonSerializerOptions()
        Dim iterator = JsonSerializer.DeserializeAsyncEnumerable(Of JsonNode)(stream, options).GetAsyncEnumerator()

        Dim counter As Integer = 0
        Do While Await iterator.MoveNextAsync()
          counter += 1
        Loop

        ' Should be in try catch, but async try catch not supported in VB.NET, only in C#
        Await iterator.DisposeAsync()

        Return counter

        'We can also use From System.Interactive.Async package
        'Return Await JsonSerializer.DeserializeAsyncEnumerable(Of JsonNode)(stream).CountAsync
      End Using
    End Function

    Private Shared Function ReadFromFile(fileName As String) As Integer
      Dim options = New JsonSerializerOptions()

      Using stream = File.OpenRead(JsonFileName)
        Return JsonSerializer.DeserializeAsyncEnumerable(Of JsonNode)(stream).ToBlockingEnumerable.Count
      End Using
    End Function

    Private Shared Iterator Function StreamElementsFromMemory() As IEnumerable(Of JsonObject)
      For i As Integer = 0 To Times - 1
        Yield New JsonObject() From
        {
            {"id", Guid.NewGuid()},
            {"FirstName", $"Bill {i}"},
            {"LastName", $"Clinton {i}"},
            {"Number", i},
            {"Address", "W. 125th street"},
            {"Place", "New York"},
            {"State", "NY"},
            {"Zip", "10027"}
        }

        Yield New JsonObject() From
          {
            {"id", Guid.NewGuid()},
            {"FirstName", $"Barack {i}"},
            {"LastName", $"Obama {i}"},
            {"Number", i},
            {"Address", "Pennsylvania Ave NW"},
            {"Place", "Washington"},
            {"State", "DC"},
            {"Zip", "20500"}
          }
      Next
    End Function

    Private Shared Sub PrintStat()
      GC.Collect()
      GC.WaitForFullGCComplete()

      Dim time = Date.Now - _startTime
      Dim size = Process.GetCurrentProcess().PrivateMemorySize64 - _startSize

      Console.WriteLine($"It took {time.TotalMilliseconds} ms.")
      Console.WriteLine($"It took {size / 1024} KBytes.")
    End Sub

    Private Shared Sub StoreStat()
      GC.Collect()
      GC.WaitForFullGCComplete()

      _startTime = Date.Now
      _startSize = Process.GetCurrentProcess().PrivateMemorySize64
    End Sub

    Private Shared Sub JsonSerializerTest()
      Dim persons = New Persons From
      {
        New Person() With
        {
          .Id = Guid.NewGuid(),
          .FirstName = "Bill",
          .LastName = "Clinton",
          .NonSerializedData = "You cannot see this"
        }
      }

      'Source generator based JsonSerializer is not available in VB.NET, only C#, use reflection based as a workaround

      Using stream = File.Create(JsonFileName2)
        JsonSerializer.Serialize(stream, persons, New JsonSerializerOptions With {.WriteIndented = True})
      End Using

      StoreStat()

      Using stream = File.OpenRead(JsonFileName)
        Dim newPersons = JsonSerializer.Deserialize(Of Persons)(stream)
        Console.WriteLine(newPersons.Count)
      End Using

      PrintStat()
    End Sub
  End Class
End Namespace
