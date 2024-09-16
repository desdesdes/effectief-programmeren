Imports System.ServiceModel
Imports System.ServiceModel.Channels

Namespace FunctionalProgramming

  Class Runner
    Shared Sub RunMe()
      QuickSort()
      'Console.WriteLine(CallRemoteDivideCalculation())
    End Sub

    Shared Sub QuickSort()
      Dim persons = New List(Of Person) From {
        New Person() With {.Id = Guid.NewGuid(), .FirstName = "Bill", .LastName = "Gates"},
        New Person() With {.Id = Guid.NewGuid(), .FirstName = "Steve", .LastName = "Ballmer"},
        New Person() With {.Id = Guid.NewGuid(), .FirstName = "Larry", .LastName = "Page"},
        New Person() With {.Id = Guid.NewGuid(), .FirstName = "Sergey", .LastName = "Brin"}
      }

      Dim sorter = New QuickSort(Of Person)

      For Each item In sorter.Sort(persons)
        Console.WriteLine("{0} {1}", item.FirstName, item.LastName)
      Next
    End Sub

    Shared Function CallRemoteDivideCalculation() As Integer
      Dim basicHttpBinding = New BasicHttpBinding
      Dim endpointAddress = New EndpointAddress("http://www.dneonline.com/calculator.asmx")

      Dim factory = New ChannelFactory(Of IPrjService)(basicHttpBinding, endpointAddress)
      Dim service = factory.CreateChannel()
      Using outgoingScope = New OperationContextScope(CType(service, IContextChannel))
        Dim messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders
        messageHeadersElement.Add(MessageHeader.CreateHeader("OnlyHandles200Response", "", True))

        Try
          Return service.Divide(12, 3)
        Catch ex As QuotaExceededException
          Throw New Exception("Message too long.")
        End Try
      End Using
    End Function
  End Class
End Namespace
