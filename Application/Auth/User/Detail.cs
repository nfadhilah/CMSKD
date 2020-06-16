using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;

namespace Application.Auth.User
{
  public class Detail
  {
    public class Query : IRequest<AppUserDto>
    {
      public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, AppUserDto>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<AppUserDto> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.AppUser.FindAsync(
          w => w.Id == request.UserId);

        if (result == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        return _mapper.Map<AppUserDto>(result);
      }
    }
  }
}