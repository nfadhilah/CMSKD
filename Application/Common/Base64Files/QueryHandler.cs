using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOS;
using AutoWrapper.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Application.Common.Base64Files
{
  public class QueryHandler : IRequestHandler<Query, object>
  {
    private const string BaseDir = "Assets";
    private readonly IWebHostEnvironment _host;
    private readonly IOptions<FileSettings> _options;

    public QueryHandler(IWebHostEnvironment host, IOptions<FileSettings> options)
    {
      _host = host;
      _options = options;
    }

    public async Task<object> Handle(Query request, CancellationToken cancellationToken)
    {
      var path = Path.Combine(_host.ContentRootPath, BaseDir, request.FileName);

      if (!File.Exists(path))
        throw new ApiException("Not Found", (int)HttpStatusCode.NotFound);

      var byteArr = await File.ReadAllBytesAsync(path, cancellationToken);

      return new
      {
        request.FileName,
        Base64 = string.Concat(request.FileType, Convert.ToBase64String(byteArr))
      };
    }
  }
}
