Namespace Exercises.Exercise2
  Public Class AntaField(Of T)
    Public Property Value As T

    ReadOnly Property SqlValue As String
      Get
        Dim stringValue = TryCast(Value, String)

        If String.IsNullOrEmpty(stringValue) Then
          Return stringValue
        End If

        Return stringValue.Replace("\'", "\'\'")
      End Get
    End Property
  End Class
End Namespace