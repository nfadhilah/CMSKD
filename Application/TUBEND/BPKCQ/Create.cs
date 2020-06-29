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

namespace Application.TUBEND.BPKCQ
{
  public class Create
  {
    public class Command : IRequest<BPK>
    {
      public long IdUnit { get; set; }
      public long IdPhk3 { get; set; }
      public string NoBPK { get; set; }
      public string KdStatus { get; set; }
      public string JBayar { get; set; }
      public int IdxKode { get; set; }
      public long IdBend { get; set; }
      public DateTime? TglBPK { get; set; }
      public string UraiBPK { get; set; }
      public DateTime? TglValid { get; set; }
      public long? IdBerita { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public string NoRef { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoBPK).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BPK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPK> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BPK>(request);

        if (!await _context.BPK.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BPK
          .FindAllAsync<DaftUnit, DaftPhk3, Bend, Berita>(
            x => x.IdBPK == added.IdBPK, x => x.Unit,
            x => x.Phk3, x => x.Bend, x => x.Berita);

        return result.SingleOrDefault();
      }
    }
  }
}