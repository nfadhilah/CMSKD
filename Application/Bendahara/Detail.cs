﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bendahara
{
  public class Detail
  {

    public class Query : IRequest<Bend>
    {
      public string KeyBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, Bend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Bend> Handle(
      Query request, CancellationToken cancellationToken)
      {
        // var result =
        //   await _context.Bend.FindByIdAsync(request.KeyBend);
            var result=
          (await _context.Bend.FindAllAsync<Pegawai>(
            x => x.KeyBend == request.KeyBend, c => c.Pegawai)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
