Imports System.Threading

Namespace ClassesAndModules
  Module Runner
    Public Sub RunMe()
      AddAndPrintValueWithModule()

      'Dim threads(10) As Thread

      'For i As Integer = 0 To threads.Length - 1
      '  threads(i) = New Thread(AddressOf AddAndPrintValueWithModule)
      'Next

      'For i As Integer = 0 To threads.Length - 1
      '  threads(i).Start()
      'Next

    End Sub

    Private Sub AddAndPrintValueWithModule()
      PrintCalls()
      PrintCalls()
      PrintCalls()
    End Sub
  End Module
End Namespace
