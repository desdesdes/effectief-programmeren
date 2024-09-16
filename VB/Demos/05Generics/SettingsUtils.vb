Imports System.IO
Imports System.Text
Imports System.Text.Json

Namespace Generics
  Module SettingsUtils
    Function Load(filePath As String) As ISettings
      Using stream = File.OpenRead(filePath)
        Dim len = stream.ReadByte()
        Dim buffer(len - 1) As Byte
        stream.Read(buffer, 0, len)
        Dim typeName = Encoding.UTF8.GetString(buffer)
        Dim result = JsonSerializer.Deserialize(stream, Type.GetType(typeName))
        Return DirectCast(result, ISettings)
      End Using
    End Function

    Public Sub Save(settings As ISettings, filePath As String)
      Using stream = File.Create(filePath)
        Dim data = Encoding.UTF8.GetBytes(settings.GetType().AssemblyQualifiedName)
        stream.WriteByte(CType(data.Length, Byte))
        stream.Write(data, 0, data.Length)
        JsonSerializer.Serialize(stream, settings, settings.GetType())
      End Using
    End Sub
  End Module
End Namespace
