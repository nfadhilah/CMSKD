using Application.Dtos;
using Application.Helpers;
using Domain;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StrukturRekening
{
    public class List
    {
        public class Query : PaginationQuery, IRequest<PaginationWrapper>
        {
            public int? MtgLevel { get; set; }
            public string NmLevel { get; set; }
        }

        public class Handler : IRequestHandler<Query, PaginationWrapper>
        {
            private readonly IDbContext _context;

            public Handler(IDbContext context)
            {
                _context = context;
            }

            public async Task<PaginationWrapper> Handle(
              Query request, CancellationToken cancellationToken)
            {
                var parameters = new List<Expression<Func<StruRek, bool>>>();

                if (request.MtgLevel.HasValue)
                    parameters.Add(d => d.MtgLevel == request.MtgLevel);

                if (!string.IsNullOrWhiteSpace(request.NmLevel))
                    parameters.Add(d => d.NmLevel.Contains(request.NmLevel));

                var predicate = PredicateBuilder.ComposeWithAnd(parameters);

                var totalItemsCount = _context.StruRek.FindAll(predicate).Count();

                var result = await _context.StruRek
                  .SetLimit(request.Limit, request.Offset)
                  .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.MtgLevel)
                  .FindAllAsync(predicate);

                return new PaginationWrapper(result, new Pagination
                {
                    CurrentPage = request.CurrentPage,
                    PageSize = request.PageSize,
                    TotalItemsCount = totalItemsCount
                });
            }
        }
    }
}
