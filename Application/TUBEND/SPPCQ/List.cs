using Application.CommonDTO;
using AutoMapper;
using MediatR;
using Persistence;
using Persistence.Repository.TUBEND;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.TUBEND.SPPCQ
{
  public class List
  {
    public class Query : PaginationQuery, ISPPSqlParam, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoSPP { get; set; }
      public string KdStatus { get; set; }
      public int? KdBulan { get; set; }
      public long? IdBend { get; set; }
      public long? IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public int? IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPP { get; set; }
      public DateTime? Status { get; set; }
      public DateTime? DateCreate { get; set; }
      public long? IdKeg { get; set; }
    }

    public class Handler : RequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      protected override PaginationWrapper Handle(Query request)
      {
        var result = _context.SPP.GetSPP(request, request.Limit, request.Offset);

        return new PaginationWrapper(_mapper.Map<IEnumerable<SPPDTO>>(result),
          new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = result.Count()
          });
      }
    }
  }
}