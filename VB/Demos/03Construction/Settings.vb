Namespace Construction
  ''' <summary>
  ''' Class to hold and pass around settings, can also load and save settings
  ''' </summary>
  Class Settings
    Public Property DcomServer As String
    Public Property ConnectionString As String

    Public Sub New()
    End Sub

    Public Sub New(filePath As String)
      Dim doc = XDocument.Load(filePath)

      DcomServer = doc.Element("Settings").Element("dcomServer").Value
      ConnectionString = doc.Element("Settings").Element("connectionString").Value
    End Sub

    Public Sub Save(filePath As String)
      Dim doc = New XElement("Settings", New XElement("dcomServer", DcomServer), New XElement("connectionString", ConnectionString))
      doc.Save(filePath)
    End Sub
  End Class
End Namespace
