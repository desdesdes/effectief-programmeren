using Microsoft.EntityFrameworkCore;

namespace Demos.Sql;

class EFCore
{
  private readonly string _connectionString;

  public EFCore(string connectionString) =>
    _connectionString = connectionString;

  public void EFCoreSelect()
  {
  }

  public void EFCoreUpdate()
  {
  }

  public void EFCoreUpdateTransaction()
  {
  }
}
