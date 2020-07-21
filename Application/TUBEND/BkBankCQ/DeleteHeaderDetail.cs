using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class DeleteHeaderDetail
  {
    public class Command : IRequest
    {
      public long IdBkBank { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(
        DeleteHeaderDetail.Command request, CancellationToken cancellationToken)
      {
        using var transaction = _context.Connection.BeginTransaction();

        try
        {
          var header =
            await _context.BkBank.FindByIdAsync(request.IdBkBank, transaction);

          if (header == null)
            throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

          var details =
            (await _context.BkBankDet.FindAllAsync(x => x.IdBkBank == header.IdBkBank, transaction)).ToList();

          if (details.Any())
          {
            var detailIds = details.Select(d => d.IdBankDet);
            await _context.BkBankDet.BulkDeleteAsync(x => x.IdBankDet,
              detailIds, transaction);
          }

          _context.BkBank.Delete(header, transaction);

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