Namespace Construction
  Class Ordering
    Dim _b As String = Log("b")
    Dim _a As String = Log("a")

    Shared _c As String = Log("c")
    Shared _d As String = Log("d")

    Shared Sub New()
      Console.WriteLine("Shared constructor")
    End Sub

    Public Sub New()
      Console.WriteLine("Constructor")
    End Sub

    Shared Function Log(data As String) As String
      Console.WriteLine(data)
      Return data
    End Function

    Public Shared Sub DoNothingShared()
      Console.WriteLine("DoNothingShared")
    End Sub

    Public Sub DoNothingInstance()
      Console.WriteLine("DoNothingInstance")
    End Sub
  End Class
End Namespace
