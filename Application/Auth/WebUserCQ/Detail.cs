using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;

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
        var result = await _context.WebUser.FindAsync(
          w => w.UserId == request.UserId);

        if (result == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        return _mapper.Map<WebUserDTO>(result);
      }
    }
  }
}
