using System.IO;
using System.Linq;

namespace Application.Common.DTOS
{
  public class FileSettings
  {
    public const string Section = "FileSettings";
    public int MaxBytes { get; set; }
    public string[] AcceptedFileTypes { get; set; }
    public bool IsSupported(string fileName)
    {
      return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
    }
  }
}