using AutoMapper;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JTahunCQ
{
  public class List
  {
    public class Query : IRequest<IEnumerable<JTahun>> { }

    public class Handler : IRequestHandler<Query, IEnumerable<JTahun>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<JTahun>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        return await _context.JTahun
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.Tahun).FindAllAsync();
      }
    }
  }
}