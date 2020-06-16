using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.Tahun
{
  public class List
  {
    public class Query : IRequest<IEnumerable<Domain.DM.Tahun>> { }

    public class Handler : IRequestHandler<Query, IEnumerable<Domain.DM.Tahun>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<Domain.DM.Tahun>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        return await _context.Tahun
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.Id).FindAllAsync();
      }
    }
  }
}