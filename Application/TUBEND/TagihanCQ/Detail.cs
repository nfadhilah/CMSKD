using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TagihanCQ
{
  public class Detail
  {
    public class Query : IRequest<TagihanDTO>
    {
      public long IdTagihan { get; set; }
    }

    public class Handler : IRequestHandler<Query, TagihanDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TagihanDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.Tagihan
            .FindAllAsync<Kontrak>(
              x => x.IdTagihan == request.IdTagihan,
              x => x.Kontrak)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<TagihanDTO>(result);
      }
    }
  }
}
