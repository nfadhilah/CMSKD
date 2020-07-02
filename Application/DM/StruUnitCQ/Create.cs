﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.StruUnitCQ
{
  public class Create
  {
    public class Command : IRequest<StruUnit>
    {
      public int KdLevel { get; set; }
      public string NmLevel { get; set; }
      public string NumDigit { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.KdLevel).NotEmpty();
        RuleFor(d => d.NmLevel).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, StruUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<StruUnit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<StruUnit>(request);

        if (!await _context.StruUnit.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
