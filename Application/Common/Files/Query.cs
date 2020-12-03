using MediatR;
using System.IO;

namespace Application.Common.Files
{
  public class Query : IRequest<MemoryStream>
  {
    public string FileType { get; set; } = "data:image/png;base64,";
    public string FileName { get; set; }
  }
}
