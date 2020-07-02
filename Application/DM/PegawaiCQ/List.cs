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

namespace Application.DM.PegawaiCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NIP { get; set; }
      public long? IdUnit { get; set; }
      public string KdGol { get; set; }
      public string Nama { get; set; }
      public string Alamat { get; set; }
      public string Jabatan { get; set; }
      public string PDDK { get; set; }
      public string NPWP { get; set; }
      public int? StAktif { get; set; }
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
        var parameters = new List<Expression<Func<Pegawai, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.NIP))
          parameters.Add(d => d.NIP.Contains(request.NIP));

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.KdGol))
          parameters.Add(d => d.KdGol == request.KdGol);

        if (!string.IsNullOrWhiteSpace(request.Nama))
          parameters.Add(d => d.Nama == request.Nama);

        if (!string.IsNullOrWhiteSpace(request.Alamat))
          parameters.Add(d => d.Alamat == request.Alamat);

        if (!string.IsNullOrWhiteSpace(request.Jabatan))
          parameters.Add(d => d.Jabatan == request.Jabatan);

        if (!string.IsNullOrWhiteSpace(request.PDDK))
          parameters.Add(d => d.PDDK == request.PDDK);

        if (!string.IsNullOrWhiteSpace(request.NPWP))
          parameters.Add(d => d.NPWP == request.NPWP);

        if (request.StAktif.HasValue)
          parameters.Add(d => d.StAktif == request.StAktif);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.Pegawai.FindAll(predicate).Count();

        var result = await _context.Pegawai
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdPeg)
          .FindAllAsync<DaftUnit, Golongan>(predicate, c => c.DaftUnit,
            c => c.Golongan);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<PegawaiDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}