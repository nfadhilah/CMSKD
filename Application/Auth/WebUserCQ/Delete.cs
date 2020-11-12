using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Auth.WebUserCQ
{
  public class Delete
  {
    public class Command : IRequest
    {
      public string UserId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
      }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var model = await _context.WebUser.FindByIdAsync(request.UserId);

        if (model == null)
          throw new ApiException("Not found",
            (int) HttpStatusCode.NotFound);

        _context.WebUser.Delete(model);

        return Unit.Value;
      }
    }
  }
}