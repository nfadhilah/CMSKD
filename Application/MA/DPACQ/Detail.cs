using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
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

    public class Query : IRequest<DPA>
    {
      public long IdDPA { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPA> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPA.FindAllAsync<DaftUnit>(x => x.IdDPA == request.IdDPA, c => c.DaftUnit)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
