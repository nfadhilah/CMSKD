﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DafPajak
{
  public class Detail
  {

    public class Query : IRequest<Pajak>
    {
      public long IdPjk { get; set; }
    }

    public class Handler : IRequestHandler<Query, Pajak>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Pajak> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.Pajak.FindByIdAsync(request.IdPjk);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
