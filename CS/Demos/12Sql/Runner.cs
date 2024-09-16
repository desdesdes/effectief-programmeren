using Microsoft.Data.SqlClient;
using System.Data;

namespace Demos.Sql;

static class Runner
{
  private const string ConnectionString = @"Server=.\profitsqldev;Database=demo;Trusted_Connection=true;TrustServerCertificate=True";

  public static void RunMe()
  {
    var efCore = new EFCore(ConnectionString);
    var dapper = new Dapper(ConnectionString);

    SqlCommandSelect();
    //SqlTableSelect();
    //dapper.DapperSelect();

    //SqlUpdate();
    //dapper.DapperUpdate();

    //efCore.EFCoreSelect();
    //efCore.EFCoreUpdate();

    //SqlUpdateTransaction();
    //dapper.DapperUpdateTransaction();
    //efCore.EFCoreUpdateTransaction();
  }

  private static void SqlCommandSelect()
  {
    var searchValue = "Bart";

    using var con = new SqlConnection(ConnectionString);
    con.Open();
    using var com = new SqlCommand($"select id, firstname, lastname from persons where firstname = '{searchValue}'", con);

    using var reader = com.ExecuteReader();
    while (reader.Read())
    {
      var person = new Person();
      person.Id = reader.GetGuid("Id");
      person.FirstName = reader.GetString("FirstName");
      person.LastName = reader.GetString("LastName");

      Console.WriteLine("{0} {1}", person.FirstName, person.LastName);
    }
  }

  private static void SqlTableSelect()
  {
    using var con = new SqlConnection(ConnectionString);
    con.Open();
    using var com = new SqlCommand("select id, firstname, lastname from persons where firstname = @firstname", con);

    com.Parameters.AddWithValue("@firstname", "Bart"); //@firstname can be replaced by firstname which also works

    using var reader = com.ExecuteReader();
    var table = new DataTable();
    table.Load(reader);

    foreach (DataRow row in table.Rows)
    {
      Console.WriteLine("{0} {1}", row["FirstName"], row["LastName"]);
    }
  }

  private static void SqlUpdate()
  {
    using var con = new SqlConnection(ConnectionString);
    con.Open();
    using var com = new SqlCommand("update persons set firstname = left(firstname, len(firstname) - 1) where firstname like '%' + @firstname", con);

    com.Parameters.AddWithValue("@firstname", "Bart%");
    Console.WriteLine($"Rows changed: {com.ExecuteNonQuery()}");
  }

  private static void SqlUpdateTransaction()
  {
    using var con = new SqlConnection(ConnectionString);
    con.Open();
    using var tran = con.BeginTransaction();

    using var com = new SqlCommand("update persons set firstname = 'Bart' where firstname = @firstname", con, tran);

    com.Parameters.AddWithValue("@firstname", "Bart%");
    Console.WriteLine($"Rows changing: {com.ExecuteNonQuery()}");

    throw new Exception("Something went wrong");

    tran.Commit(); //if this line is not called, the changes are not committed
  }
}
