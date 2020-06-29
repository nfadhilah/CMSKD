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

namespace Application.TUBEND.BeritaDetRCQ
{
  public class Create
  {
    public class Command : IRequest<BeritaDetR>
    {
      public long IdBerita { get; set; }
      public long IdRek { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBerita).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BeritaDetR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BeritaDetR> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BeritaDetR>(request);

        if (!await _context.BeritaDetR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BeritaDetR
          .FindAllAsync<Berita, DaftRekening>(
            x => x.IdBeritaDet == added.IdBeritaDet, x => x.Berita,
            x => x.Rekening);

        return result.SingleOrDefault();
      }
    }
  }
}