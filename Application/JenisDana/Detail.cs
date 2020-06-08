using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisDana
{
  public class Detail
  {

    public class Query : IRequest<JDana>
    {
      public long IdJDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, JDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JDana> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JDana.FindByIdAsync(request.IdJDana);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
