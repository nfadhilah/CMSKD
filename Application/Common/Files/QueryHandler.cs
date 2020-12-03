using AutoWrapper.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Files
{
  public class QueryHandler : IRequestHandler<Query, MemoryStream>
  {
    private const string BaseDir = "Assets";
    private readonly IWebHostEnvironment _host;

    public QueryHandler(IWebHostEnvironment host)
    {
      _host = host;
    }

    public async Task<MemoryStream> Handle(Query request, CancellationToken cancellationToken)
    {
      var path = Path.Combine(_host.ContentRootPath, BaseDir, request.FileName);

      if (!File.Exists(path))
        throw new ApiException("Not Found", (int)HttpStatusCode.NotFound);

      var memory = new MemoryStream();

      await using var stream = new FileStream(path, FileMode.Open);

      await stream.CopyToAsync(memory, cancellationToken);

      memory.Position = 0;

      return memory;
    }
  }
}
