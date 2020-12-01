using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftDokCQ
{
  public class Detail
  {
    public class Query : IRequest<DaftDok>
    {
      public string KdDok { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftDok>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<DaftDok> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.DaftDok.FindAsync(
          w => w.KdDok == request.KdDok);

        if (result == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
