using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABCQ
{
  public class Create
  {
    public class Command : IRequest<DPABDTO>
    {
      public long IdDPA { get; set; }
      public int? IdXKode { get; set; }
      public string KdTahap { get; set; }
      public long IdRek { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPA).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.IdXKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPABDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPAB>(request);

        if (!await _context.DPAB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.DPAB.FindAllAsync<DPA, DaftRekening>(
            x => x.IdDPAB == added.IdDPAB, x => x.DPA, x => x.DaftRekening);

        return _mapper.Map<DPABDTO>(result.Single());
      }
    }
  }
}