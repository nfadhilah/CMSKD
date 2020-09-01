using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Dapper;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.MA.KegUnitCQ
{
  public class Induk
  {
    public class Query : IRequest<object>
    {
      public long IdUnit { get; set; }
      public string KdTahap { get; set; }
      public long IdPrgrm { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.IdUnit).NotEmpty();
        RuleFor(x => x.KdTahap).NotEmpty();
        RuleFor(x => x.IdPrgrm).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, object>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<object> Handle(
        Query request, CancellationToken cancellationToken)
      {
        return await _context.KegUnit.GetGroupKegUnitByKegInduk(request.IdUnit,
          request.IdPrgrm, request.KdTahap);
      }
    }
  }
}