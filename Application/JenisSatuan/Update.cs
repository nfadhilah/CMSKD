using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisSatuan
{
  public class Update
  {
    public class Command : IRequest
    {
      public long IdSatuan { get; set; }
      public string KdSatuan { get; set; }
      public string UraiSatuan { get; set; }
      public string Ket { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSatuan).NotEmpty();
        RuleFor(d => d.KdSatuan).NotEmpty();
        RuleFor(d => d.UraiSatuan).NotEmpty();
        RuleFor(d => d.Ket).NotEmpty();
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
        var updated =
          await _context.JSatuan.FindByIdAsync(request.IdSatuan);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JSatuan.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}