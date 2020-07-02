using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPCQ
{
  public class Create
  {
    public class Command : IRequest<DPDTO>
    {
      public string NoDP { get; set; }
      public int? IdxKode { get; set; }
      public long? IdTtd { get; set; }
      public DateTime? TglDP { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.NoDP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DP>(request);

        if (!await _context.DP.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<DPDTO>(added);
      }
    }
  }
}