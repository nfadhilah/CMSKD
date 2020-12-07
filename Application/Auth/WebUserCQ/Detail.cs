using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.WebUserCQ
{
  public class Detail
  {
    public class Query : IRequest<WebUserDTO>
    {
      public string UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, WebUserDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<WebUserDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.WebUser.FindAsync<WebGroup>(
          w => w.UserId == request.UserId, x => x.WebGroup);


        if (result == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        return _mapper.Map<WebUserDTO>(result);
      }
    }
  }
}
