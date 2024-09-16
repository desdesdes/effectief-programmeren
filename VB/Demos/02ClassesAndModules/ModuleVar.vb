Imports System.Threading

Namespace ClassesAndModules

  Module ModuleVar
    Dim _counter As Integer = 1

    Sub PrintCalls()
      Console.WriteLine("{0} has value: {1}", Thread.CurrentThread.ManagedThreadId, _counter)
      _counter += 1
    End Sub
  End Module
End Namespace
