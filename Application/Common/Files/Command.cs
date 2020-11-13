using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Files
{
  public class Command : IRequest<object>
  {
    public string FileType { get; set; } = "data:image/png;base64,";
    public IFormFile File { get; set; }
  }

  public class Validator : AbstractValidator<Command>
  {
    public Validator()
    {
      RuleFor(x => x.File).NotEmpty();
    }
  }
}