using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
  public class List
  {
    public class Query : IRequest<IEnumerable<WebUser>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WebUser>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<WebUser>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        using var transaction = _context.Connection.BeginTransaction();

        return await _context.WebUser.FindAllAsync();
      }
    }
  }
}