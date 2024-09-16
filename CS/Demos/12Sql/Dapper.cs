using Microsoft.Data.SqlClient;
using Dapper;

namespace Demos.Sql;

class Dapper
{
  private readonly string _connectionString;

  public Dapper(string connectionString)
  {
    _connectionString = connectionString;
  }

  public void DapperSelect()
  {
    using var con = new SqlConnection(_connectionString);
    var persons = con.Query<Person>("select id, firstname, lastname from persons where firstname = @firstname", new { FirstName = "Bart" });

    foreach (var person in persons)
    {
      Console.WriteLine("{0} {1}", person.FirstName, person.LastName);
    }
  }

  public void DapperUpdate()
  {
    using var con = new SqlConnection(_connectionString);
    var rowsAffected = con.Execute("update persons set firstname = left(firstname, len(firstname) - 1) where firstname like '%' + @firstname", new { FirstName = "Bart%" });
    Console.WriteLine($"Rows changing: {rowsAffected}");
  }

  public void DapperUpdateTransaction()
  {
    using var con = new SqlConnection(_connectionString);
    con.Open();

    using var tran = con.BeginTransaction();
    var rowsAffected = con.Execute("update persons set firstname = 'Bart' where firstname = @firstname", new { FirstName = "Bart%" }, tran);
    Console.WriteLine($"Rows changing: {rowsAffected}");

    tran.Commit(); //if this line is not called, the changes are not committed
  }
}
