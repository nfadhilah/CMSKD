using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.KontrakCQ
{
  public class Create
  {
    public class Command : IRequest<KontrakDTO>
    {
      public long IdUnit { get; set; }
      public string NoKontrak { get; set; }
      public long IdPhk3 { get; set; }
      public long IdKeg { get; set; }
      public DateTime? TglKon { get; set; }
      public DateTime? TglAwalKontrak { get; set; }
      public DateTime? TglAkhirKontrak { get; set; }
      public DateTime? TglSlsKontrak { get; set; }
      public string Uraian { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoKontrak).NotEmpty();
        RuleFor(d => d.IdPhk3).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, KontrakDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KontrakDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Kontrak>(request);

        if (!await _context.Kontrak.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.Kontrak
          .FindAllAsync<DaftUnit, DaftPhk3, MKegiatan>(
            x => x.IdKontrak == added.IdKontrak, x => x.Unit,
            x => x.Phk3, x => x.Kegiatan);

        return _mapper.Map<KontrakDTO>(result.SingleOrDefault());
      }
    }
  }
}