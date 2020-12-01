using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TagihanDetCQ
{
  public class Detail
  {
    public class Query : IRequest<TagihanDetDTO>
    {
      public long IdTagihanDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, TagihanDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TagihanDetDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.TagihanDet
            .FindAllAsync<DaftRekening>(
              x => x.IdTagihanDet == request.IdTagihanDet,
              x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<TagihanDetDTO>(result);
      }
    }
  }
}
