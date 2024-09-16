using System.IO;

namespace Exercises.Exercise1;

static class File
{
  public static void Write(string path, string contents)
  {
    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    System.IO.File.WriteAllText(path, contents);
    Thread.Sleep(4000);
  }
}
