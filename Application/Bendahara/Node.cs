using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bendahara
{
  public class Node
  {
    public class Query : IRequest<IEnumerable<Bend>>
    {
      public int? StAktif { get; set; }
      public string KdBank { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Bend>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<Bend>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var mapConfig = new MapperConfiguration(cfg => { });

        return _mapper.Map<IEnumerable<Bend>>(
          await _context.Bend.GetBendNodes(request.StAktif,
            request.KdBank));
      }
    }
  }
}