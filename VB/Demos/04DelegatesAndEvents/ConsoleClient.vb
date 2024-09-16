Namespace DelegatesAndEvents
  Class ConsoleClient
    Public Sub Print()
      QueuePrintJob("Hallo world!")

      Console.WriteLine("ConsoleClient: Print finished")

      Console.WriteLine("Press any key to exit")
      Console.ReadKey()
    End Sub
  End Class
End Namespace
