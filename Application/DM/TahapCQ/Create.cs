using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.TahapCQ
{
  public class Create
  {
    public class Command : IRequest<Tahap>
    {
      public string KdTahap { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglTransfer { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
        RuleFor(d => d.TglTransfer).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Tahap>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Tahap> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Tahap>(request);

        if (!await _context.Tahap.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}