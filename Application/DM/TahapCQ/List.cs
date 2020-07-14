using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.TahapCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string Uraian { get; set; }
      public DateTime? TglTransfer { get; set; }
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
        Query req, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<Tahap, bool>>>();

        if (!string.IsNullOrWhiteSpace(req.Uraian))
          parameters.Add(x => x.Uraian.Contains(req.Uraian));

        if (req.TglTransfer.HasValue)
          parameters.Add(x => x.TglTransfer == req.TglTransfer);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result = await _context.Tahap.FindAllAsync(predicate);

        return new PaginationWrapper(result,
          new Pagination
          {
            CurrentPage = req.CurrentPage,
            PageSize = req.PageSize,
            TotalItemsCount = result.Count()
          });
      }
    }
  }
}