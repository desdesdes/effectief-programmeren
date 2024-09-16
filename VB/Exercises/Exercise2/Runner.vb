Namespace Exercises.Exercise2
  Class Runner
    Shared Sub RunMe()
      Dim persons = New AntaDataBuffer(Of PersonRow)

      ''Commentaar opgave 1
      'Dim personRow = New PersonRow()
      'personRow.Id.Value = Guid.NewGuid()
      'personRow.FirstName.Value = "Larry"
      'personRow.LastName.Value = "Page"
      'persons.Add(personRow)

      'Dim myGuid = Guid.NewGuid()

      'personRow = New PersonRow()
      'personRow.Id.Value = myGuid
      'personRow.FirstName.Value = "Sergey"
      'personRow.LastName.Value = "Brin"
      'persons.Add(personRow)

      'Console.WriteLine("List:")

      'For Each person In persons
      '  Console.WriteLine("{0} {1}", person.FirstName.Value, person.LastName.Value)
      'Next

      '''Commentaar opgave 2
      'Console.WriteLine()
      'Console.WriteLine("Indexer:")
      'Console.WriteLine("{0} {1}", persons(myGuid).FirstName.Value, persons(myGuid).LastName.Value)
    End Sub
  End Class
End Namespace
