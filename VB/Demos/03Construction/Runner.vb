Imports System.Runtime.Loader

Namespace Construction
  Module Runner
    Sub RunMe()
      StartSettings()
      'StartOrdering()
      'StartFinalize()
      'StartSingleton()
    End Sub

    Private Sub StartSettings()
      Dim settings = New Settings()
      settings.DcomServer = "pwbvr"

      settings.Save("c:\temp\demo.dat")

      settings = New Settings("c:\temp\demo.dat")

      Console.WriteLine(settings.DcomServer)
    End Sub

    Private Sub StartOrdering()
      Console.WriteLine("Startup")
      Ordering.DoNothingShared()
      Console.WriteLine("Ready")
    End Sub

    Private Sub StartFinalize()
      Dim x As New RefHolder(1)
      Dim y As New RefHolder(2)
      x.Reference = y
      y.Reference = x
    End Sub

    Private Sub StartSingleton()
      IncAndPrintSingleton(Nothing)
    End Sub

    Private Sub IncAndPrintSingleton(stateInfo As Object)
      dim value = Singleton.Current.SingletonCounter

      Console.WriteLine(value)

      Singleton.Current.SingletonCounter = value + 1
    End Sub
  End Module
End Namespace
