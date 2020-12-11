using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TU;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.BKUSTSCQ
{
  public class Create
  {
    public class Command : IRequest<BKUSTS>
    {
      public string UnitKey { get; set; }
      public string NoBKUSKPD { get; set; }
      public string NoSTS { get; set; }
      public string IdxTtd { get; set; }
      public DateTime? TglBKUSKPD { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
      public string KeyBend { get; set; }
    }

    public class Handler : IRequestHandler<Command, BKUSTS>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUSTS> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BKUSTS>(request);

        if (!await _context.BKUSTS.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BKUSTS.FindAsync(x => x.UnitKey == added.UnitKey && x.NoBKUSKPD == added.NoBKUSKPD);

        return result;
      }
    }
  }
}