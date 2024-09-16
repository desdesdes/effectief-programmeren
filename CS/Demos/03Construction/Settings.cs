using System.Xml.Linq;

namespace Demos.Construction;

class Settings
{
  public string? DcomServer { get; set; }
  public string? ConnectionString { get; set; }

  public Settings()
  {
  }

  public Settings(string filePath)
  {
    var doc = XDocument.Load(filePath);

    DcomServer = doc?.Element("Settings")?.Element("dcomServer")?.Value;
    ConnectionString = doc?.Element("Settings")?.Element("connectionString")?.Value;
  }

  public void Save(string filePath)
  {
    var doc = new XElement("Settings", new XElement("dcomServer", DcomServer), new XElement("connectionString", ConnectionString));
    doc.Save(filePath);
  }
}
