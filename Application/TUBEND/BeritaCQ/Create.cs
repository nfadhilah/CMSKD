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

namespace Application.TUBEND.BeritaCQ
{
  public class Create
  {
    public class Command : IRequest<Berita>
    {
      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public string NoBerita { get; set; }
      public DateTime TglBA { get; set; }
      public long IdKontrak { get; set; }
      public string Urai_Berita { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.NoBerita).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.TglBA).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdKontrak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Berita>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Berita> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Berita>(request);

        if (!await _context.Berita.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.Berita
          .FindAllAsync<DaftUnit, MKegiatan, Kontrak>(
            x => x.IdBerita == added.IdBerita, x => x.Unit,
            x => x.Kegiatan, x => x.Kontrak);

        return result.SingleOrDefault();
      }
    }
  }
}