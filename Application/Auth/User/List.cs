using AutoMapper;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Auth.User
{
  public class List
  {
    public class Query : IRequest<IEnumerable<WebUserDto>>
    {
      public long? IdUnit { get; set; }
      public long? GroupId { get; set; }
      public List<string> ExcludedRoleName { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WebUserDto>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IUserAccessor _userAccessor;

      public Handler(
        IDbContext context, IMapper mapper, IUserAccessor userAccessor)
      {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
      }

      public async Task<IEnumerable<WebUserDto>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.WebUser.GetUsers(
          _userAccessor.GetCurrentAppId(),
          request.IdUnit,
          request.GroupId, request.ExcludedRoleName);

        return _mapper.Map<IEnumerable<WebUserDto>>(result);
      }
    }
  }
}