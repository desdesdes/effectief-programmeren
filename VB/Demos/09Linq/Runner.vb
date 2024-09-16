Namespace Linq
  Class Runner
    Public Shared Sub RunMe()
      Dim persons As New List(Of Person)()

      LoadData(persons)

      Dim newPersons = persons

      For Each person In newPersons
        Console.WriteLine($"{person.FirstName} {person.LastName}")
      Next
    End Sub

    Private Shared Sub LoadData(persons As List(Of Person))
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Brain", .LastName = "Harry"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Bob", .LastName = "Beauchemin"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Craig", .LastName = "Freedman"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Don", .LastName = "Box"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Ian", .LastName = "Griffith"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Jeff", .LastName = "Beehler"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Joel", .LastName = "Pobar"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Rob", .LastName = "Mensching"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Robert", .LastName = "McLaws"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Sara", .LastName = "Ford"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Scott", .LastName = "Guthrie"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Robert", .LastName = "McLaws"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Brad", .LastName = "Adams"})
      persons.Add(New Person() With {.Id = Guid.NewGuid(), .FirstName = "Chris", .LastName = "Anderson"})
    End Sub
  End Class
End Namespace
