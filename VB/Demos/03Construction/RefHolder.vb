Public Class RefHolder
  Private ReadOnly _number As Integer

  Sub New (number as Integer)
    _number = number
  End Sub

  Property Reference as RefHolder

  Protected Overrides Sub Finalize()
    Console.WriteLine($"Finalize: {_number}")
  End Sub
End Class
