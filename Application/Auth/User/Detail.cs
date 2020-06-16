using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.User
{
  public class Detail
  {
    public class Query : IRequest<WebUserDto>
    {
      public string UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, WebUserDto>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<WebUserDto> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.WebUser.FindAsync(
          w => w.UserId == request.UserId);

        if (result == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        return _mapper.Map<WebUserDto>(result);
      }
    }
  }
}