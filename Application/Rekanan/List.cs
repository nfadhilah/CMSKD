using Domain;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class List
  {
    public class Query : IRequest<IEnumerable<DaftPhk3>>
    {
      public string Kdp3 { get; set; }
      public string Nmp3 { get; set; }
      public string Nminst { get; set; }
      public string Norcp3 { get; set; }
      public string Nmbank { get; set; }
      public string Jnsusaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public string Unitkey { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<DaftPhk3>>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<IEnumerable<DaftPhk3>> Handle(Query request, CancellationToken cancellationToken)
      {
        return await _context.DaftPhk3.SetOrderBy(OrderInfo.SortDirection.ASC, d => d.Kdp3).FindAllAsync();
      }
    }
  }
}
