using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.KodeBank
{
  public class Detail
  {

    public class Query : IRequest<JBank>
    {
      public long IdJBank { get; set; }
    }

    public class Handler : IRequestHandler<Query, JBank>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBank> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JBank.FindByIdAsync(request.IdJBank);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
