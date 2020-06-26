using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;

namespace Application.DM.BendCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdPeg { get; set; }
      public string JnsBend { get; set; }
      public string RekBend { get; set; }
      public string IdBank { get; set; }
      public long? IdUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<PaginationWrapper> Handle(
        Query req, CancellationToken cancellationToken)
      {
        var totalItemsCount = (await _context.Bend.GetBend(req.IdPeg,
          req.JnsBend, req.RekBend, req.IdBank, req.IdUnit)).Count();

        var result = await _context.Bend.GetBend(req.IdPeg,
          req.JnsBend, req.RekBend, req.IdBank, req.IdUnit);

        return new PaginationWrapper(result, new Pagination
        {
          CurrentPage = req.CurrentPage,
          PageSize = req.PageSize,
          TotalItemsCount = totalItemsCount
        });
      }
    }
  }
}