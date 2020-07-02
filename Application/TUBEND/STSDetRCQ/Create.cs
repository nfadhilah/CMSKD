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

namespace Application.TUBEND.STSDetRCQ
{
  public class Create
  {
    public class Command : IRequest<STSDetRDTO>
    {
      public long IdSTS { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSTS).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, STSDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<STSDetR>(request);

        if (!await _context.STSDetR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.STSDetR
          .FindAllAsync<STS, MKegiatan, DaftRekening>(
            x => x.IdSTSDetR == added.IdSTSDetR,
            x => x.STS, x => x.Kegiatan, x => x.Rekening);

        return _mapper.Map<STSDetRDTO>(result.SingleOrDefault());
      }
    }
  }
}