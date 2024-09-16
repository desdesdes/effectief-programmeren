namespace Demos.Sql;

static class SqlEscape
{
  public static string LikeEscape(string s) =>
    s.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
}
