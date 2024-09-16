Imports Exercises.Exercises.Exercise1

Public Class MainWindow
  Private Sub CloseButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub StartButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles StartButton.Click
    File.Write("c:\temp\demo.txt", "Hallo wereld")
    TextBox1.Text = "De file is geschreven"
  End Sub
End Class
