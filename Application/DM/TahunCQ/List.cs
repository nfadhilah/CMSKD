using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.TahunCQ
{
  public class List
  {
    public class Query : IRequest<IEnumerable<Tahun>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Tahun>>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<IEnumerable<Tahun>> Handle(Query request, CancellationToken cancellationToken)
      {
        var result = await _context.Tahun
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.NmTahun).FindAllAsync();

        return result;
      }
    }
  }
}