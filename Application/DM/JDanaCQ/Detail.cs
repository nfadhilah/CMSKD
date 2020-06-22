﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JDanaCQ
{
  public class Detail
  {

    public class Query : IRequest<JDana>
    {
      public string KdDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, JDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JDana> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JDana.FindAsync(x => x.KdDana == request.KdDana);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}