using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.PM.UserKegiatanCQ
{
  public class Tree
  {
    public class Query : IRequest<IEnumerable<UserKegiatanTreeDTO>>
    {
      public long IdUnit { get; set; }
      public int KdTahap { get; set; }
      public bool? IsSelected { get; set; }
    }

    public class
      Handler : IRequestHandler<Query, IEnumerable<UserKegiatanTreeDTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<UserKegiatanTreeDTO>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var configuration = new MapperConfiguration(cfg => { });

        var result =
          await _context.UserKegiatan.GetTreeUserKegiatan(request.IdUnit,
            request.KdTahap,
            request.IsSelected);

        return _mapper.Map<IEnumerable<UserKegiatanTreeDTO>>(result);
      }
    }
  }
}