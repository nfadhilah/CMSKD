using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.PM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.PM.DiskusiPaketCQ
{
  public class List
  {
    public class QueryDTO : IMapDTO<Query>
    {
      private readonly IMapper _mapper;

      public int? IdDiskusiPaket { get; set; }
      public string Komentar { get; set; }
      public string Sender { get; set; }

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

    public class Query : IRequest<IEnumerable<DiskusiPaket>>
    {
      public int? IdDiskusiPaket { get; set; }
      public string Komentar { get; set; }
      public string Sender { get; set; }
      public long IdRUP { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<DiskusiPaket>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<DiskusiPaket>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<DiskusiPaket, bool>>>();

        if (request.IdDiskusiPaket.HasValue)
          parameters.Add(x => x.IdDiskusiPaket == request.IdDiskusiPaket);

        if (!string.IsNullOrWhiteSpace(request.Komentar))
          parameters.Add(x => x.Komentar.Contains(request.Komentar));

        if (!string.IsNullOrWhiteSpace(request.Sender))
          parameters.Add(x => x.Sender == request.Sender);

        parameters.Add(x => x.IdRUP == request.IdRUP);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result = await _context.DiskusiPaket
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.DateCreate)
          .FindAllAsync(predicate);

        return result;
      }
    }
  }
}