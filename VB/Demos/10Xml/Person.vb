Imports System.Xml.Serialization

Namespace Xml
  Public Class Person
    <XmlAttribute("id")>
    Public Property Id As Guid
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Number As Integer
    Public Property Address As String
    Public Property Place As String
    Public Property State As String
    Public Property Zip As String
  End Class
End Namespace
