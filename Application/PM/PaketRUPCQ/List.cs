﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using Domain.PM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.PM.PaketRUPCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdRUP { get; set; }
      public long? IdUnit { get; set; }
      public long? IdKeg { get; set; }
      public long? IdSubKeg { get; set; }
      public decimal? NilaiPagu { get; set; }
      public long? IdProgram { get; set; }
      public DateTime? TglValid { get; set; }
      public JnsRUP? JnsRUP { get; set; }
      public int? TipeSwakelola { get; set; }
      public string UraiTipeSwakelola { get; set; }
      public bool? IsDraft { get; set; }
      public bool? IsDraftFinalized { get; set; }
      public bool? IsAnnounced { get; set; }
      public bool? IsRevised { get; set; }
      public bool? IsCanceled { get; set; }
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
        var parameters = new List<Expression<Func<Domain.PM.PaketRUP, bool>>>();

        if (request.IdRUP.HasValue)
          parameters.Add(x => x.IdRUP == request.IdRUP);

        if (request.IdUnit.HasValue)
          parameters.Add(x => x.IdUnit == request.IdUnit);

        if (request.IdKeg.HasValue)
          parameters.Add(x => x.Keg.IdKegInduk == request.IdKeg);

        if (request.IdSubKeg.HasValue)
          parameters.Add(x => x.IdKeg == request.IdSubKeg);

        if (request.NilaiPagu.HasValue)
          parameters.Add(x => x.NilaiPagu == request.NilaiPagu);

        if (request.TglValid.HasValue)
          parameters.Add(x => x.TglValid == request.TglValid);

        if (request.IdProgram.HasValue)
          parameters.Add(x => x.Keg.IdPrgrm == request.IdProgram);

        if (request.JnsRUP.HasValue)
          parameters.Add(x => x.JnsRUP == request.JnsRUP);

        if (request.TipeSwakelola.HasValue)
          parameters.Add(x => x.TipeSwakelola == request.TipeSwakelola);

        if (!string.IsNullOrWhiteSpace(request.UraiTipeSwakelola))
          parameters.Add(x =>
            x.UraiTipeSwakelola.Contains(request.UraiTipeSwakelola));

        if (request.IsDraft.HasValue)
          parameters.Add(x => x.A.Value);

        if (request.IsDraftFinalized.HasValue)
          parameters.Add(x => x.A.Value && x.FD.Value);

        if (request.IsAnnounced.HasValue)
          parameters.Add(x => x.A.Value && x.FD.Value && x.U.Value);

        if (request.IsRevised.HasValue)
          parameters.Add(x => !x.A.Value && x.FD.Value && x.U.Value);

        if (request.IsCanceled.HasValue)
          parameters.Add(x => !x.A.Value && !x.FD.Value && !x.U.Value);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.PaketRup.FindAll().Count();

        var result = await _context.PaketRup
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdRUP)
          .FindAllAsync<DaftUnit, MKegiatan, JPekerjaan, JDana, DaftPhk3>(
            predicate,
            c => c.Unit,
            c => c.Keg, c => c.JnsPekerjaan, c => c.JDana, c => c.Phk3);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<PaketRUPDTO>>(result),
          new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}