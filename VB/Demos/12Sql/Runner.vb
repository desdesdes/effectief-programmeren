Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Sql
  Class runner
    Private Const ConnectionString As String = "Server=.\profitsqldev;Database=demo;Trusted_Connection=true;TrustServerCertificate=True"

    Public Shared Sub RunMe()
      Dim efCore = New EFCore(ConnectionString)
      Dim dapper = New Dapper(ConnectionString)

      SqlCommandSelect()
      'SqlTableSelect()
      'dapper.DapperSelect()

      'SqlUpdate()
      'dapper.DapperUpdate()

      'efCore.EFCoreSelect()
      'efCore.EFCoreUpdate()

      'SqlUpdateTransaction()
      'dapper.DapperUpdateTransaction()
      'efCore.EFCoreUpdateTransaction()
    End Sub

    Shared Sub SqlCommandSelect()
      Dim searchValue = "Bart"

      Using con = New SqlConnection(ConnectionString)
        con.Open()
        Using com = New SqlCommand($"select id, firstname, lastname from persons where firstname = '{searchValue}'", con)

          Using reader = com.ExecuteReader()
            While reader.Read()
              Dim person = New Person()
              person.Id = reader.GetGuid("Id")
              person.FirstName = reader.GetString("FirstName")
              person.LastName = reader.GetString("LastName")

              Console.WriteLine("{0} {1}", person.FirstName, person.LastName)
            End While
          End Using
        End Using
      End Using
    End Sub

    Shared Sub SqlTableSelect()
      Using con = New SqlConnection(ConnectionString)
        con.Open()
        Using com = New SqlCommand("select id, firstname, lastname from persons where firstname = @firstname", con)

          com.Parameters.AddWithValue("@firstname", "Bart") '@firstname can be replaced by firstname which also works

          Using reader = com.ExecuteReader()
            Dim table = New DataTable()
            table.Load(reader)

            For Each row As DataRow In table.Rows
              Console.WriteLine("{0} {1}", row("FirstName"), row("LastName"))
            Next
          End Using
        End Using
      End Using
    End Sub

    Private Shared Sub SqlUpdate()
      Using con = New SqlConnection(ConnectionString)
        con.Open()
        Using com = New SqlCommand("update persons set firstname = left(firstname, len(firstname) - 1) where firstname like '%' + @firstname", con)

          com.Parameters.AddWithValue("@firstname", "Bart%")
          Console.WriteLine($"Rows changed: {com.ExecuteNonQuery()}")

        End Using
      End Using
    End Sub

    Shared Sub SqlUpdateTransaction()
      Using con = New SqlConnection(ConnectionString)
        con.Open()
        Using tran = con.BeginTransaction()

          Using com = New SqlCommand("update persons set firstname = 'Bart' where firstname = @firstname", con, tran)

            com.Parameters.AddWithValue("@firstname", "Bart%")
            Console.WriteLine($"Rows changing: {com.ExecuteNonQuery()}")

          End Using

          Throw New Exception("Something went wrong")

          tran.Commit() 'if this line is not called, the changes are not committed
        End Using
      End Using
    End Sub
  End Class
End Namespace
