Imports System.Runtime.Intrinsics
Imports System.Windows.Threading
Imports Demos.DelegatesAndEvents

Public Class WpfClient
  Private Sub StartButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles StartButton.Click
    'AddHandler Printer.PrintFinished, AddressOf OnPrintFinished
    QueuePrintJob("Hallo world!")
    End Sub

  'Private Sub OnPrintFinished(sender As Object, e As PrintFinishedEventArgs)
  '  TextBox1.Text = "WpfClient: OnPrintFinished called"
  'End Sub

  Private Sub CloseButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles CloseButton.Click
    Close()
  End Sub
End Class
