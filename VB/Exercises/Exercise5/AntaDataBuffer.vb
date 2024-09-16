Namespace Exercises.Exercise5
  Class AntaDataBuffer(Of T)
    Implements ICollection(Of T)

    Private _rows As List(Of T) = New List(Of T)()
    Private _idSelector As Func(Of T, Guid)

    Public Sub New(idSelector As Func(Of T, Guid))
      _idSelector = idSelector
    End Sub

    Public Sub Add(item As T) Implements ICollection(Of T).Add
      _rows.Add(item)
    End Sub

    Public Sub Clear() Implements ICollection(Of T).Clear
      _rows.Clear()
    End Sub

    Public Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
      Return _rows.Contains(item)
    End Function

    Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
      _rows.CopyTo(array, arrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count
      Get
        Return _rows.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
      Return _rows.Remove(item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
      Return _rows.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      Return _rows.GetEnumerator()
    End Function

    Default Public ReadOnly Property Item(index As Guid) As T
      Get
        For Each row In _rows
          If _idSelector(row) = index Then
            Return row
          End If
        Next

        Return Nothing
      End Get
    End Property
  End Class
End Namespace
