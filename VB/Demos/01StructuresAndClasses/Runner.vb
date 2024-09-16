Namespace StructuresAndClasses

  Structure Point
    Public Property X As Integer
    Public Property Y As Integer
  End Structure

  Module Runner
    Public Sub RunMe()
     Dim p = New Point() With {.X = 3}

      Move(p)

      Console.WriteLine($"X: {p.X}, Y: {p.Y}")
    End Sub

    Public Sub Move(point As Point)
      point.X += 1
      point.Y += 1
    End Sub
  End Module

End Namespace
