using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPDetCQ
{
  public class Create
  {
    public class Command : IRequest<DPDet>
    {
      public long IdDP { get; set; }
      public long IdSP2D { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDP).NotEmpty();
        RuleFor(d => d.IdSP2D).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPDet>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPDet> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPDet>(request);

        if (!await _context.DPDet.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}