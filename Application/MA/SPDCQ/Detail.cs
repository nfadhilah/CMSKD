﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDCQ
{
  public class Detail
  {
    public class Query : IRequest<SPD>
    {
      public long IdSPD { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPD>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPD> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.SPD.FindByIdAsync(request.IdSPD);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}