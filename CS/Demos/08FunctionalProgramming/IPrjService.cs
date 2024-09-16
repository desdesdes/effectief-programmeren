using System.ServiceModel;

namespace Demos.FunctionalProgramming;

[ServiceContract]
public interface IPrjService
{
  [OperationContract(Action = "http://tempuri.org/Divide")]
  int Divide(int intA, int intB);
  [OperationContract(Action = "http://tempuri.org/Add")]
  int Add(int intA, int intB);
  [OperationContract(Action = "http://tempuri.org/Multiply")]
  int Multiply(int intA, int intB);
  [OperationContract(Action = "http://tempuri.org/Subtract")]
  int Subtract(int intA, int intB);
}
