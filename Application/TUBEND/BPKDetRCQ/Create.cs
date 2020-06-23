﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKDetRCQ
{
  public class Create
  {
    public class Command : IRequest<BPKDetR>
    {
      public long IdBPK { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBPK).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BPKDetR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetR> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BPKDetR>(request);

        if (!await _context.BPKDetR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}