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

namespace Application.DM.SifatKegCQ
{
	public class List
	{
		public class Query : PaginationQuery, IRequest<PaginationWrapper>
		{
			public string KdSifat { get; set; }
			public string NmSifat { get; set; }
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
				var parameters = new List<Expression<Func<SifatKeg, bool>>>();

				if (!string.IsNullOrWhiteSpace(request.KdSifat))
					parameters.Add(d => d.KdSifat.Contains(request.KdSifat));

				if (!string.IsNullOrWhiteSpace(request.NmSifat))
					parameters.Add(d => d.NmSifat.Contains(request.NmSifat));

				var predicate = PredicateBuilder.ComposeWithAnd(parameters);

				var totalItemsCount = _context.SifatKeg.FindAll(predicate).Count();

				var result = await _context.SifatKeg
				  .SetLimit(request.Limit, request.Offset)
				  .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSifatKeg)
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
