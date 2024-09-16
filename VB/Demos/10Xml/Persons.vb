Imports System.Xml.Serialization

Namespace Xml
  <XmlRoot("Persons")>
  Public Class Persons
    Inherits List(Of Person)
  End Class
End Namespace
