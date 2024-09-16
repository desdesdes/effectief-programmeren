Imports System.Threading

Namespace Exercises.Exercise1
  Module File
    Public Sub Write(path As String, contents As String)
      IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(path))
      IO.File.WriteAllText(path, contents)
      Thread.Sleep(4000)
    End Sub
  End Module
End Namespace
