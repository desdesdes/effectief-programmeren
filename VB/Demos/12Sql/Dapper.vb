Imports Microsoft.Data.SqlClient
Imports Dapper

Namespace Sql
  Public Class Dapper
    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
      _connectionString = connectionString
    End Sub

    Public Sub DapperSelect()
      Using con = New SqlConnection(_connectionString)
        Dim persons = con.Query(Of Person)("select id, firstname, lastname from persons where firstname = @firstname", New With {.FirstName = "Bart"})

        For Each person In persons
          Console.WriteLine("{0} {1}", person.FirstName, person.LastName)
        Next
      End Using
    End Sub

    Public Sub DapperUpdate()
      Using con = New SqlConnection(_connectionString)
        Dim rowsAffected = con.Execute("update persons set firstname = left(firstname, len(firstname) - 1) where firstname like '%' + @firstname", New With {.FirstName = "Bart%"})
        Console.WriteLine($"Rows changing: {rowsAffected}")
      End Using
    End Sub

    Public Sub DapperUpdateTransaction()
      Using con = New SqlConnection(_connectionString)
        con.Open()

        Using tran = con.BeginTransaction()
          Dim rowsAffected = con.Execute("update persons set firstname = 'Bart' where firstname = @firstname", New With {.FirstName = "Bart%"}, tran)
          Console.WriteLine($"Rows changing: {rowsAffected}")

          tran.Commit() 'if this line is not called, the changes are not committed
        End Using
      End Using
    End Sub
  End Class
End Namespace
