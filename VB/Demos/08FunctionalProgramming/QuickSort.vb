Namespace FunctionalProgramming
  Public Class QuickSort(Of T As IComparable(Of T))
    Public Function Sort(list As IList(Of T)) As IList(Of T)
      Dim newData = New List(Of T)(list.Count)

      dim pos = 0

      while pos < list.Count
        dim item = list(pos)

        For index = 0 To pos
          if index = pos then
            newData.Add(item)
            Exit For
          else if item.CompareTo(newData(index)) < 0 then
            newData.Insert(index, item)
            Exit For
          End If
        Next

        pos += 1
      End While

      Return newData
    End Function
  End Class
End Namespace
