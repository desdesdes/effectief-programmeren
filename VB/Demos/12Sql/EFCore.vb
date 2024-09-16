Imports Microsoft.EntityFrameworkCore

Namespace Sql

  Public Class EFCore
    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
      _connectionString = connectionString
    End Sub

    Public Sub EFCoreSelect()

    End Sub

    Public Sub EFCoreUpdate()

    End Sub

    Public Sub EFCoreUpdateTransaction()

    End Sub

  End Class
End Namespace
