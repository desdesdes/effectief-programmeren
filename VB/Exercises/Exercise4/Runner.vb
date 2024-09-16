Namespace Exercises.Exercise4
  Class Runner
    Private Shared _startTime As Date
    Private Shared _startSize As Long
    Private Const XmlFileName = "c:\temp\demo.xml"

    Public Shared Sub RunMe()
      Dim persons = New AntaDataBuffer(Of PersonRow)(Function(row) row.Id.Value)

      StoreStat()

      Dim elements = XElement.Load(XmlFileName).Elements()

      'Zet de bovenstaande code regel onder commentaar en haal het commentaar bij de
      'onderstaande regel weg. Implementeer de StreamXml functie
      'Dim elements = StreamXml(XmlFileName)

      PrintStat()

      For Each personElement In elements
        Dim personRow = New PersonRow()
        personRow.Id.Value = New Guid(personElement.Attribute("id").Value)
        personRow.FirstName.Value = personElement.Element("FirstName").Value
        personRow.LastName.Value = personElement.Element("LastName").Value
        persons.Add(personRow)
      Next

      PrintStat()
      Console.WriteLine("Loaded {0} persons.", persons.Count)
      Console.ReadKey()

      Console.WriteLine("List:")
      For Each person In persons
        Console.WriteLine("{0} {1}", person.FirstName.Value, person.LastName.Value)
      Next
    End Sub

    Private Shared Sub StoreStat()
      GC.Collect()
      GC.WaitForFullGCComplete()

      _startTime = Date.Now
      _startSize = Process.GetCurrentProcess().PrivateMemorySize64
    End Sub

    Private Shared Sub PrintStat()
      GC.Collect()
      GC.WaitForFullGCComplete()

      Dim time = Date.Now - _startTime
      Dim size = Process.GetCurrentProcess().PrivateMemorySize64 - _startSize

      Console.WriteLine("It took {0} ms.", time.TotalMilliseconds)
      Console.WriteLine("It took {0} KBytes.", size / 1024)
    End Sub

    Private Shared Function StreamXml(uri As String) As IEnumerable(Of XElement)
      Return Nothing
    End Function
  End Class
End Namespace
