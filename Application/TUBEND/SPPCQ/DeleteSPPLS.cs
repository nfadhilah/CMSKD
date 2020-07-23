using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPCQ
{
  public class DeleteSPPLS
  {
    public class Command : IRequest
    {
      public long IdSPP { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        using var transaction = _context.Connection.BeginTransaction();

        try
        {
          var spp =
            await _context.SPP.FindByIdAsync(request.IdSPP, transaction);

          if (spp == null)
            throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

          if (spp.TglValid.HasValue)
            throw new ApiException(
              "Data tidak bisa dihapus karena sudah valid");

          await _context.SPPBA.DeleteAsync(x => x.IdSPP == spp.IdSPP, transaction);

          await _context.SPPDetR.DeleteAsync(x => x.IdSPP == spp.IdSPP, transaction);

          await _context.SPP.DeleteAsync(spp, transaction);

          transaction.Commit();

          return Unit.Value;
        }
        catch (Exception e)
        {
          transaction.Rollback();
          throw new ApiException(e.Message);
        }
      }
    }
  }
}