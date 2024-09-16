Imports Demos.Collecties

Namespace ExtensionMethods
  Module Runner
    Public Sub RunMe()
      Dim p1 = New Person() With {.Id = Guid.NewGuid(), .FirstName = "Bill", .LastName = "Gates"}
      Dim p2 = New Person() With {.Id = Guid.NewGuid(), .FirstName = "Steve", .LastName = "Ballmer"}

      Console.WriteLine(GetFullName(p1))
    End Sub

    Function GetFullName(p As Person) As String
      Return $"{p.FirstName} {p.LastName}"
    End Function
  End Module

End Namespace
