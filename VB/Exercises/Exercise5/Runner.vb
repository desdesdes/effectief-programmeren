Imports System.Xml

Namespace Exercises.Exercise5
  Class Runner
    Private Const _connectionString As String = "Server=.\profitsqldev;Database=demo;Trusted_Connection=true;TrustServerCertificate=True"
    Private Const _xmlFileName = "c:\temp\demo.xml"

    Public Shared Sub RunMe()
      Dim persons = New AntaDataBuffer(Of PersonRow)(Function(row) row.Id.Value)

      Dim elements = StreamXml(_xmlFileName)

      For Each personElement In elements.Take(100)
        Dim personRow = New PersonRow()
        personRow.Id.Value = New Guid(personElement.Attribute("id").Value)
        personRow.FirstName.Value = personElement.Element("FirstName").Value
        personRow.LastName.Value = personElement.Element("LastName").Value
        persons.Add(personRow)

        WriteToSql(personRow)
      Next

      Console.WriteLine("Loaded {0} persons.", persons.Count)
      Console.ReadKey()

    End Sub


    Private Shared Sub WriteToSql(personRow As PersonRow)

    End Sub

    Private Shared Iterator Function StreamXml(uri As String) As IEnumerable(Of XElement)
      Using reader As XmlReader = XmlReader.Create(uri)
        reader.ReadStartElement("Persons")

        While reader.Read()
          If (reader.NodeType = XmlNodeType.Element) Then
            Yield CType(XElement.ReadFrom(reader), XElement)
          End If
        End While

        reader.Close()
      End Using
    End Function
  End Class
End Namespace
