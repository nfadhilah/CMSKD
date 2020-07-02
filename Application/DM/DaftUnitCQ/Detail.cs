﻿using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftUnitCQ
{
  public class Detail
  {
    public class Query : IRequest<DaftUnitDTO>
    {
      public int IdUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftUnitDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftUnitDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftUnit.FindByIdAsync(request.IdUnit);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DaftUnitDTO>(result);
      }
    }
  }
}
