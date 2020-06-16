using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Auth.User
{
  public class List
  {
    public class Query : IRequest<IEnumerable<AppUserDto>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<AppUserDto>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<AppUserDto>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.AppUser.FindAllAsync();

        return _mapper.Map<IEnumerable<AppUserDto>>(result);
      }
    }
  }
}