using Application.CommonDTO;
using AutoMapper;
using MediatR;
using Persistence;
using Persistence.Repository.TUBEND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BeritaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IBeritaSqlParam, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdKeg { get; set; }
      public string NoBerita { get; set; }
      public DateTime? TglBA { get; set; }
      public long? IdKontrak { get; set; }
      public string Urai_Berita { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
      public DateTime? DateCreate { get; set; }
      public bool IsValid { get; set; }
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
        var result =
          await _context.Berita.GetBerita(request, request.Offset, request.Limit);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<BeritaDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = result.Count()
          });
      }
    }
  }
}