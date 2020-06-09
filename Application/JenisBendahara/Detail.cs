﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisBendahara
{
  public class Detail
  {

    public class Query : IRequest<JBend>
    {
      public long IdJBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, JBend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBend> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JBend.FindAsync(x => x.IdJBend == request.IdJBend);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
