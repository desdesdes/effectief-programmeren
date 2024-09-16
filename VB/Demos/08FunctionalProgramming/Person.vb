Namespace FunctionalProgramming
  Class Person
    Implements IComparable(Of Person)

    Property Id As Guid
    Property FirstName As String
    Property LastName As String


    Public Function CompareTo(other As Person) As Integer Implements IComparable(Of Person).CompareTo
      Return FirstName.CompareTo(other.FirstName)
    End Function
  End Class
End Namespace
