using AutoMapper;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tahun
{
  public class List
  {
    public class Query : IRequest<IEnumerable<Domain.Tahun>> { }

    public class Handler : IRequestHandler<Query, IEnumerable<Domain.Tahun>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<Domain.Tahun>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        return await _context.Tahun
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.Id).FindAllAsync();
      }
    }
  }
}