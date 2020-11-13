using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOS;
using AutoWrapper.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Application.Common.Files
{
  public class CommandHandler : IRequestHandler<Command, object>
  {
    private const string BaseDir = "Assets";
    private readonly IWebHostEnvironment _host;
    private readonly FileSettings _fileSettings;

    public CommandHandler(IWebHostEnvironment host, IOptionsSnapshot<FileSettings> options)
    {
      _host = host;
      _fileSettings = options.Value;
    }

    public async Task<object> Handle(Command request, CancellationToken cancellationToken)
    {
      if (request.File.Length > _fileSettings.MaxBytes)
        throw new ApiException("Max file excedeed");

      if (!_fileSettings.IsSupported(request.File.FileName))
        throw new ApiException("Invalid file type");

      var baseDir = Path.Combine(_host.ContentRootPath, BaseDir);

      if (!Directory.Exists(baseDir))
        Directory.CreateDirectory(baseDir);

      var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
      
      var filePath = Path.Combine(baseDir, fileName);

      // await using (var writer = new FileStream(filePath, FileMode.Create))
      // {
      //   await request.File.CopyToAsync(writer, cancellationToken);
      // }

      // await using (var stream = new MemoryStream())
      // {
      //   var reader = new FileStream(filePath, FileMode.Open);
      //
      //   await reader.CopyToAsync(stream, cancellationToken);
      //
      //   return new
      //   {
      //     FileName = fileName,
      //     Base64 = string.Concat(request.FileType, Convert.ToBase64String(stream.ToArray()))
      //   };
      // }

      await using (var stream = new MemoryStream())
      {
        var reader = request.File.OpenReadStream();

        await reader.CopyToAsync(stream, cancellationToken);

        return new
        {
          FileName = fileName, Base64 = string.Concat(request.FileType, Convert.ToBase64String(stream.ToArray()))
        };
      }
    }
  }
}
