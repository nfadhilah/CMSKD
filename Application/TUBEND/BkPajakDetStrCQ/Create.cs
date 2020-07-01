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

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class Create
  {
    public class Command : IRequest<BkPajakDetStrDTO>
    {
      public long IdBkPajakStr { get; set; }
      public long? IdPajak { get; set; }
      public long IdBkPajak { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglIdBilling { get; set; }
      public DateTime? TglExpire { get; set; }
      public string NTPN { get; set; }
      public string NTB { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBkPajakStr).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
        RuleFor(d => d.IdBkPajak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BkPajakDetStrDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkPajakDetStrDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkPajakDetStr>(request);

        if (!await _context.BkPajakDetStr.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BkPajakDetStr
          .FindAllAsync<BPKPajakStr, Pajak, BkPajak>(
            x => x.IdBkPajakDetStr == added.IdBkPajakDetStr,
            x => x.BPKPajakStr, x => x.Pajak, x => x.BkPajak);

        return _mapper.Map<BkPajakDetStrDTO>(result.SingleOrDefault());
      }
    }
  }
}