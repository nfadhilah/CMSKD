using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRPCQ
{
  public class Create
  {
    public class Command : IRequest<SP2DDetRPDTO>
    {
      public long IdSP2DDetR { get; set; }
      public long IdPajak { get; set; }
      public decimal? Nilai { get; set; }
      public string Keterangan { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglBilling { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSP2DDetR).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2DDetRPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2DDetRP>(request);

        if (!await _context.SP2DDetRP.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetRP
          .FindAllAsync<SP2DDetR, Pajak>(
            x => x.IdSP2DDetRP == added.IdSP2DDetRP, x => x.SP2DDetR,
            x => x.Pajak);

        return _mapper.Map<SP2DDetRPDTO>(result.SingleOrDefault());
      }
    }
  }
}