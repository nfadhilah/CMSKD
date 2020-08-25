﻿using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.JPekerjaanCQ
{
  public class Detail
  {
    public class Query : IRequest<JPekerjaan>
    {
      public long IdJnsPekerjaan { get; set; }
    }

    public class Handler : IRequestHandler<Query, JPekerjaan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JPekerjaan> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JPekerjaan.FindByIdAsync(request.IdJnsPekerjaan);

        if (result == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}