Imports System.ServiceModel

Namespace FunctionalProgramming
  <ServiceContract>
  Public Interface IPrjService
    <OperationContract(Action:="http://tempuri.org/Divide")>
    Function Divide(intA as integer, intB as Integer) As Integer
    <OperationContract(Action:="http://tempuri.org/Add")>
    Function Add(intA as integer, intB as Integer) As Integer
    <OperationContract(Action:="http://tempuri.org/Multiply")>
    Function Multiply(intA as integer, intB as Integer) As Integer
    <OperationContract(Action:="http://tempuri.org/Subtract")>
    Function Subtract(intA as integer, intB as Integer) As Integer
  End Interface
End Namespace
