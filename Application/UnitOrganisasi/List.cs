﻿using AutoMapper;
using Domain;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitOrganisasi
{
  public class List
  {
    public class Query : IRequest<IEnumerable<DaftUnit>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<DaftUnit>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<DaftUnit>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        return await _context.DaftUnit
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdUnit)
          .FindAllAsync();
      }
    }
  }
}