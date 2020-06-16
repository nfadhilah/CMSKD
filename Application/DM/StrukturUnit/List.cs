using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.StrukturUnit
{
    public class List
    {
        public class Query : PaginationQuery, IRequest<PaginationWrapper>
        {
            public int? KdLevel { get; set; }
            public string NmLevel { get; set; }
            public string NumDigit { get; set; }
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
                var parameters = new List<Expression<Func<StruUnit, bool>>>();

                if (request.KdLevel.HasValue)
                    parameters.Add(d => d.KdLevel == request.KdLevel);

                if (!string.IsNullOrWhiteSpace(request.NmLevel))
                    parameters.Add(d => d.NmLevel.Contains(request.NmLevel));

                if (!string.IsNullOrWhiteSpace(request.NumDigit))
                    parameters.Add(d => d.NumDigit.Contains(request.NumDigit));

                var predicate = PredicateBuilder.ComposeWithAnd(parameters);

                var totalItemsCount = _context.StruUnit.FindAll(predicate).Count();

                var result = await _context.StruUnit
                  .SetLimit(request.Limit, request.Offset)
                  .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdLevel)
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
