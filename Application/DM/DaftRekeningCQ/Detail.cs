﻿using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftRekeningCQ
{
  public class Detail
  {
    public class Query : IRequest<DaftRekeningDTO>
    {
      public long IdRek { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftRekeningDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftRekeningDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftRekening.FindByIdAsync(request.IdRek);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DaftRekeningDTO>(result);
      }
    }
  }
}
