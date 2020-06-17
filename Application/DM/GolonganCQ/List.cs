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

namespace Application.DM.GolonganCQ
{
    public class List
    {
        public class Query : PaginationQuery, IRequest<PaginationWrapper>
        {
            public string KdGol { get; set; }
            public string NmGol { get; set; }
            public string Pangkat { get; set; }
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
                var parameters = new List<Expression<Func<Golongan, bool>>>();

                if (!string.IsNullOrWhiteSpace(request.KdGol))
                    parameters.Add(d => d.KdGol.Contains(request.KdGol));

                if (!string.IsNullOrWhiteSpace(request.NmGol))
                    parameters.Add(d => d.NmGol.Contains(request.NmGol));

                if (!string.IsNullOrWhiteSpace(request.Pangkat))
                    parameters.Add(d => d.Pangkat == request.Pangkat);

                var predicate = PredicateBuilder.ComposeWithAnd(parameters);

                var totalItemsCount = _context.Golongan.FindAll(predicate).Count();

                var result = await _context.Golongan
                  .SetLimit(request.Limit, request.Offset)
                  .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdGol)
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
