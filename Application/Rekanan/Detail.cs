﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class Detail
  {

    public class Query : IRequest<DaftPhk3>
    {
      public string KdP3 { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftPhk3>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftPhk3.FindByIdAsync(request.KdP3);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
