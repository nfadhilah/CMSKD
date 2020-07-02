﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.StruRekCQ
{
  public class Create
  {
    public class Command : IRequest<StruRek>
    {
      public int MtgLevel { get; set; }
      public string NmLevel { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.MtgLevel).NotEmpty();
        RuleFor(d => d.NmLevel).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, StruRek>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<StruRek> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<StruRek>(request);

        if (!await _context.StruRek.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
