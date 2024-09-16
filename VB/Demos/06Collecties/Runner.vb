Namespace Collecties
  Module Runner
    Sub RunMe()
      Dim persons As New PersonCollection

      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Bill", .LastName = "Gates"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Steve", .LastName = "Ballmer"})

      'For Each item In persons
      '  Console.WriteLine($"First: {item.FirstName}, Last {item.LastName}")
      'Next

      'Console.WriteLine($"First: {persons(1).FirstName}, Last {persons(1).LastName}")
    End Sub
  End Module
End Namespace
