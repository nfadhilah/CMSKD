﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class Create
  {
    public class Command : IRequest<BkBank>
    {
      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public string NoBuku { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBuku { get; set; }
      public string Uraian { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.NoBuku).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BkBank>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkBank> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkBank>(request);

        if (!await _context.BkBank.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}