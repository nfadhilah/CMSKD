using MediatR;

namespace Application.Common.Base64Files
{
  public class Query : IRequest<object>
  {
    public string FileType { get; set; } = "data:image/png;base64,";
    public string FileName { get; set; }
  }
}
