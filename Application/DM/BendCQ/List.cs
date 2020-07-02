using Application.CommonDTO;
using AutoMapper;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaginationWrapper> Handle(
        Query req, CancellationToken cancellationToken)
      {
        var totalItemsCount = (await _context.Bend.GetBend(req.IdPeg,
          req.JnsBend, req.RekBend, req.IdBank, req.IdUnit)).Count();

        var result = await _context.Bend.GetBend(req.IdPeg,
          req.JnsBend, req.RekBend, req.IdBank, req.IdUnit);

        return new PaginationWrapper(_mapper.Map<IEnumerable<BendDTO>>(result),
          new Pagination
          {
            CurrentPage = req.CurrentPage,
            PageSize = req.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}