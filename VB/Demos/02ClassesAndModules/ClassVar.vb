Imports System.Threading

Namespace ClassesAndModules

  Class ClassVar
    Dim _counter As Integer = 1

    Sub PrintCalls()
      Console.WriteLine($"{Environment.CurrentManagedThreadId} has value: {_counter}")
      _counter += 1
    End Sub
  End Class
End Namespace
