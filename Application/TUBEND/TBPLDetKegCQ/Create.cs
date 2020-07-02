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

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class Create
  {
    public class Command : IRequest<TBPLDetKegDTO>
    {
      public long IdTBPLDet { get; set; }
      public int IdNoJeTra { get; set; }
      public long IdKeg { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdTBPLDet).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, TBPLDetKegDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDetKegDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<TBPLDetKeg>(request);

        if (!await _context.TBPLDetKeg.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.TBPLDetKeg
          .FindAllAsync<TBPLDet, JTrnlKas, MKegiatan>(
            x => x.IdTBPLDet == added.IdTBPLDetKeg, x => x.TBPLDet,
            x => x.JTrnlKas, x => x.Kegiatan);

        return _mapper.Map<TBPLDetKegDTO>(result.SingleOrDefault());
      }
    }
  }
}