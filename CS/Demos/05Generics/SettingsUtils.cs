using System.IO;
using System.Text;
using System.Text.Json;

namespace Demos.Generics;

static class SettingsUtils
{
  public static ISettings Load(string filePath)
  {
    using var stream = File.OpenRead(filePath);
    var len = stream.ReadByte();
    var buffer = new byte[len];
    _ = stream.Read(buffer, 0, len); // Discard the result, we know it's the same as len
    var typeName = Encoding.UTF8.GetString(buffer);
    var result = JsonSerializer.Deserialize(stream, Type.GetType(typeName)!);
    return (ISettings)result!;
  }

  public static void Save(ISettings settings, string filePath)
  {
    using var stream = File.Create(filePath);
    var data = Encoding.UTF8.GetBytes(settings.GetType().AssemblyQualifiedName!);
    stream.WriteByte((byte)data.Length);
    stream.Write(data, 0, data.Length);
    JsonSerializer.Serialize(stream, settings, settings.GetType());
  }
}
