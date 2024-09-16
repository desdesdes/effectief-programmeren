Imports System.Threading

Namespace DelegatesAndEvents
  Module Printer
    Public Sub QueuePrintJob(data As String)
      ThreadPool.QueueUserWorkItem(AddressOf DoPrint, data)
    End Sub

    Private Sub DoPrint(state As Object)
      Console.WriteLine("Printer: DoPrint started")
      Thread.Sleep(2000)
      Console.WriteLine($"Printer: Printing {state}")

      Console.WriteLine("Printer: DoPrint ready")
    End Sub
  End Module
End Namespace
