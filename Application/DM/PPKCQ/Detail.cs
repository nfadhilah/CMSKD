﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.PPKCQ
{
  public class Detail
  {

    public class Query : IRequest<PPK>
    {
      public long IdPPK { get; set; }
    }

    public class Handler : IRequestHandler<Query, PPK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PPK> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.PPK.FindAllAsync<Pegawai>(
            x => x.IdPPK == request.IdPPK, c => c.Pegawai)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}