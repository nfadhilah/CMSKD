﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Files
{
  public class Command : IRequest<object>
  {
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