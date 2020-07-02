using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftBankCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string AkBank { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string Cabang { get; set; }
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
        var parameters = new List<Expression<Func<DaftBank, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.AkBank))
          parameters.Add(d => d.AkBank.Contains(request.AkBank));

        if (!string.IsNullOrWhiteSpace(request.Alamat))
          parameters.Add(d => d.Alamat.Contains(request.Alamat));

        if (!string.IsNullOrWhiteSpace(request.Telepon))
          parameters.Add(d => d.Telepon.Contains(request.Telepon));

        if (!string.IsNullOrWhiteSpace(request.Cabang))
          parameters.Add(d => d.Cabang.Contains(request.Cabang));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DaftBank.FindAll(predicate).Count();

        var result = await _context.DaftBank
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdBank)
          .FindAllAsync(predicate);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<DaftBankDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}