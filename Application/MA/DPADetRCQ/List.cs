﻿using Application.Helpers;
using Domain.MA;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;

namespace Application.MA.DPADetRCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdDPAR { get; set; }
      public string KdNilai { get; set; }
      public string KdJabar { get; set; }
      public string Uraian { get; set; }
      public Decimal? JumBYek { get; set; }
      public string Satuan { get; set; }
      public Decimal? Tarif { get; set; }
      public Decimal? SubTotal { get; set; }
      public string Ekspresi { get; set; }
      public byte? InclSubtotal { get; set; }
      public string Type { get; set; }
      public string IdStdHarga { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
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
        var parameters = new List<Expression<Func<DPADetR, bool>>>();

        if (request.IdDPAR.HasValue)
          parameters.Add(d => d.IdDPAR == request.IdDPAR);
        if (!string.IsNullOrWhiteSpace(request.KdNilai))
          parameters.Add(d => d.KdNilai == request.KdNilai);
        if (!string.IsNullOrWhiteSpace(request.KdJabar))
          parameters.Add(d => d.KdJabar == request.KdJabar);
        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian == request.Uraian);
        if (request.JumBYek.HasValue)
          parameters.Add(d => d.JumBYek == request.JumBYek);
        if (!string.IsNullOrWhiteSpace(request.Satuan))
          parameters.Add(d => d.Satuan == request.Satuan);
        if (request.Tarif.HasValue)
          parameters.Add(d => d.Tarif == request.Tarif);
        if (request.SubTotal.HasValue)
          parameters.Add(d => d.SubTotal == request.SubTotal);
        if (!string.IsNullOrWhiteSpace(request.Ekspresi))
          parameters.Add(d => d.Ekspresi == request.Ekspresi);
        if (request.InclSubtotal.HasValue)
          parameters.Add(d => d.InclSubtotal == request.InclSubtotal);
        if (!string.IsNullOrWhiteSpace(request.Type))
          parameters.Add(d => d.Type == request.Type);
        if (!string.IsNullOrWhiteSpace(request.IdStdHarga))
          parameters.Add(d => d.IdStdHarga == request.IdStdHarga);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DPADetR.FindAll(predicate).Count();

        var result = await _context.DPADetR
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdDPADetR)
          .FindAllAsync<DPAR>(predicate, c => c.DPAR);

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