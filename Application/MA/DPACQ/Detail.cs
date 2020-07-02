using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPACQ
{
  public class Detail
  {

    public class Query : IRequest<DPADTO>
    {
      public long IdDPA { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPA.FindAllAsync<DaftUnit>(
            x => x.IdDPA == request.IdDPA, c => c.DaftUnit)).FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPADTO>(result);
      }
    }
  }
}
