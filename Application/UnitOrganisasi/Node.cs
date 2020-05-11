using AutoMapper;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitOrganisasi
{
  public class Node
  {
    public class Query : IRequest<IEnumerable<DaftUnitNode>>
    {
      public int? KdLevel { get; set; }
      public string KdUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<DaftUnitNode>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<DaftUnitNode>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var mapConfig = new MapperConfiguration(cfg => { });

        return _mapper.Map<IEnumerable<DaftUnitNode>>(
          await _context.DaftUnit.GetDaftUnitNodes(request.KdLevel,
            request.KdUnit));
      }
    }
  }
}