using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.KecamatanCQ
{
  public class List
  {
    public class QueryDTO
    {
      private readonly IMapper _mapper;

      public string IdKec { get; set; }
      public string Nama { get; set; }

      public QueryDTO()
      {
        var config =
          new MapperConfiguration(opt => opt.CreateMap<QueryDTO, Query>());

        _mapper = config.CreateMapper();
      }

      public Query MapDTO(Query destination)
      {
        return _mapper.Map(this, destination);
      }
    }

    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string IdKabKota { get; set; }
      public string IdKec { get; set; }
      public string Nama { get; set; }
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
        Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<Kecamatan, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.IdKabKota))
          parameters.Add(x => x.IdKabKota == request.IdKabKota);

        if (!string.IsNullOrWhiteSpace(request.Nama))
          parameters.Add(x => x.Nama == request.Nama);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result = await _context.Kecamatan
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.IdKec)
          .FindAllAsync(predicate);

        return new PaginationWrapper(result, new Pagination
        {
          CurrentPage = request.CurrentPage,
          PageSize = request.PageSize,
          TotalItemsCount = result.Count()
        });
      }
    }
  }
}