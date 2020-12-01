﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABCQ
{
  public class Detail
  {
    public class Query : IRequest<DPABDTO>
    {
      public long IdDPAB { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPABDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPAB.FindAllAsync<DPA, DaftRekening>(
            x => x.IdDPAB == request.IdDPAB, c => c.DPA, c => c.DaftRekening))
          .SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPABDTO>(result);
      }
    }
  }
}