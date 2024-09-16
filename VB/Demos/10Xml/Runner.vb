Imports System.IO
Imports System.Xml

Namespace Xml
  Class Runner
    Private Shared _startTime As Date
    Private Shared _startSize As Long
    Private Const _xmlFileName = "c:\temp\demo.xml"
    Private Const _xmlFileName2 = "c:\temp\demo2.xml"

    Private Const _times As Integer = 200000

    Public Shared Sub RunMe()
      DocumentSpeedTest()
      'OptimizedReadSpeedTest()
      'BuildUsingXmlWriter()
      'BuildUsingXElement()
      'WriteTransformTest()
      'XmlSerializerTest()
    End Sub

    Private Shared Sub DocumentSpeedTest()
      StoreStat()

      Dim document = New XmlDocument()
      document.Load(_xmlFileName)
      Console.WriteLine(document.DocumentElement.ChildNodes.Count)

      'Dim document = XDocument.Load(XmlFileName)
      'Console.WriteLine(document.Elements().Elements().Count())

      PrintStat()

      Console.WriteLine(document.DocumentElement.ChildNodes.Count)
      'Console.WriteLine(document.Elements().Elements().Count())
    End Sub

    Private Shared Sub OptimizedReadSpeedTest()
      StoreStat()

      Dim query = StreamElementsFromFile(_xmlFileName)

      Console.WriteLine(query.Count())

      PrintStat()

      Console.WriteLine(query.Count())
    End Sub

    Private Shared Sub BuildUsingXmlWriter()
      StoreStat()

      Dim settings = New XmlWriterSettings With {
        .Indent = True
      }

      Using writer = XmlWriter.Create(_xmlFileName, settings)
        writer.WriteStartDocument()
        writer.WriteStartElement("Persons")
        For i As Integer = 0 To _times - 1 'ca 100mb
          writer.WriteStartElement("Person")
          writer.WriteAttributeString("id", Guid.NewGuid().ToString())
          writer.WriteElementString("FirstName", $"Bill {i}")
          writer.WriteElementString("LastName", $"Clinton {i}")
          writer.WriteElementString("Number", i.ToString())
          writer.WriteElementString("Address", "W. 125th street")
          writer.WriteElementString("Place", "New York")
          writer.WriteElementString("State", "NY")
          writer.WriteElementString("Zip", "10027")
          writer.WriteEndElement()

          writer.WriteStartElement("Person")
          writer.WriteAttributeString("id", Guid.NewGuid().ToString())
          writer.WriteElementString("FirstName", $"Barack {i}")
          writer.WriteElementString("LastName", $"Obama {i}")
          writer.WriteElementString("Number", i.ToString())
          writer.WriteElementString("Address", "Pennsylvania Ave NW")
          writer.WriteElementString("Place", "Washington")
          writer.WriteElementString("State", "DC")
          writer.WriteElementString("Zip", "20500")
          writer.WriteEndElement()
        Next
      End Using

      PrintStat()
    End Sub

    Private Shared Sub BuildUsingXElement()
      StoreStat()

      Dim newXML = New XElement("Persons", StreamElementsFromMemory())

      'PrintStat()

      newXML.Save(_xmlFileName)

      PrintStat()
    End Sub

    Private Shared Sub WriteTransformTest()
      StoreStat()

      Dim newXML =
        New XStreamingElement("NewPersons",
          From element In StreamElementsFromFile(_xmlFileName)
          Where element.Element("State").Value = "NY"
          Select New XElement("NewPerson",
            New XAttribute("FirstName", element.Element("FirstName").Value),
            New XAttribute("LastName", element.Element("LastName").Value)))

      newXML.Save(_xmlFileName2)

      PrintStat()
    End Sub

    Private Shared Iterator Function StreamElementsFromFile(fileName As String) As IEnumerable(Of XElement)
      Using reader = XmlReader.Create(fileName)
        reader.ReadStartElement("Persons")

        While reader.Read()
          If (reader.NodeType = XmlNodeType.Element) AndAlso (reader.Name = "Person") Then
            Yield CType(XElement.ReadFrom(reader), XElement)
          End If
        End While

        reader.Close()
      End Using
    End Function

    Private Shared Iterator Function StreamElementsFromMemory() As IEnumerable(Of XElement)
      For i As Integer = 0 To _times - 1
        Yield New XElement("Person",
          New XAttribute("id", Guid.NewGuid()),
          New XElement("FirstName", $"Bill {i}"),
          New XElement("LastName", $"Clinton {i}"),
          New XElement("Number", i),
          New XElement("Address", "W. 125th street"),
          New XElement("Place", "New York"),
          New XElement("State", "NY"),
          New XElement("Zip", "10027"))

        Yield New XElement("Person",
          New XAttribute("id", Guid.NewGuid().ToString()),
          New XElement("FirstName", $"Barack {i}"),
          New XElement("LastName", $"Obama {i}"),
          New XElement("Number", i),
          New XElement("Address", "Pennsylvania Ave NW"),
          New XElement("Place", "Washington"),
          New XElement("State", "DC"),
          New XElement("Zip", "20500"))
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

    Private Shared Sub XmlSerializerTest()
      Dim persons = New Persons From
      {
        New Person() With
        {
          .Id = Guid.NewGuid(),
          .FirstName = "Bill",
          .LastName = "Clinton"
        }
      }

      Dim serializer = New Serialization.XmlSerializer(GetType(Persons))
      Using stream = File.Create(_xmlFileName2)
        serializer.Serialize(stream, persons)
      End Using

      StoreStat()

      Using stream = File.OpenRead(_xmlFileName)
        Dim newPersons = CType(serializer.Deserialize(stream), Persons)
        Console.WriteLine(newPersons.Count)
      End Using

      PrintStat()
    End Sub
  End Class
End Namespace
