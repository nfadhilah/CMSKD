using AutoMapper;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.User
{
  public class List
  {
    public class Query : IRequest<IEnumerable<WebUserDto>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WebUserDto>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<WebUserDto>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.WebUser.FindAllAsync();

        return _mapper.Map<IEnumerable<WebUserDto>>(result);
      }
    }
  }
}