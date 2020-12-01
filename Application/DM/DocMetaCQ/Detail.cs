using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DocMetaCQ
{
  public class Detail
  {
    public class Query : IRequest<DocMeta>
    {
      public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, DocMeta>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<DocMeta> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.DocMeta.FindAsync(
          w => w.Id == request.Id);

        if (result == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
