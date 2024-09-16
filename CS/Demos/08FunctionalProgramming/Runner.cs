using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Demos.FunctionalProgramming;

static class Runner
{
  public static void RunMe()
  {
    QuickSort();
    //Console.WriteLine(CallRemoteDivideCalculation());
  }

  static void QuickSort()
  {
    var persons = new List<Person>
    {
      new Person(Guid.NewGuid(), "Bill", "Gates"),
      new Person(Guid.NewGuid(), "Steve", "Ballmer"),
      new Person(Guid.NewGuid(), "Larry", "Page"),
      new Person(Guid.NewGuid(), "Sergey", "Brin")
    };

    var sorter = new QuickSort<Person>();

    foreach (var item in sorter.Sort(persons))
    {
      Console.WriteLine($"{item.FirstName} {item.LastName}");
    }
  }

  static int CallRemoteDivideCalculation()
  {
    var basicHttpBinding = new BasicHttpBinding();
    var endpointAddress = new EndpointAddress("http://www.dneonline.com/calculator.asmx");

    var factory = new ChannelFactory<IPrjService>(basicHttpBinding, endpointAddress);
    var service = factory.CreateChannel();
    using (var outgoingScope = new OperationContextScope((IContextChannel)service))
    {
      var messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
      messageHeadersElement.Add(MessageHeader.CreateHeader("OnlyHandles200Response", "", true));

      try
      {
        return service.Divide(12, 3);
      }
      catch (QuotaExceededException)
      {
        throw new Exception("Message too long.");
      }
    }
  }
}
