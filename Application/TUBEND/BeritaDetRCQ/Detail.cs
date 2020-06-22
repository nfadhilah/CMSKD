﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BeritaDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<BeritaDetR>
    {
      public long IdBeritaDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, BeritaDetR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BeritaDetR> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.BeritaDetR.FindByIdAsync(request.IdBeritaDet);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
