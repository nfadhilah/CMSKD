using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Auth;
using Domain.DM;
using Domain.PM;
using MediatR;
using Persistence;

namespace Application.PM.UserKegiatanCQ
{
  public class List
  {
    public class QueryDTO : IMapDTO<Query>
    {
      private readonly IMapper _mapper;

      public List<long> IdKeg { get; set; }

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

    public class Query : IRequest<IEnumerable<UserKegiatanDTO>>
    {
      public string UserId { get; set; }
      public List<long> IdKeg { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<UserKegiatanDTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<UserKegiatanDTO>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.UserKegiatan.GetListUserKegiatan(request.UserId,
            request.IdKeg);

        return _mapper.Map<IEnumerable<UserKegiatanDTO>>(result);
      }
    }
  }
}