using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
  public class Detail
  {
    public class Query : IRequest<WebUser>
    {
      public string UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, WebUser>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<WebUser> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.WebUser.FindAsync<WebGroup>(
          w => w.UserId == request.UserId, g => g.WebGroup);

        return result;
      }
    }
  }
}