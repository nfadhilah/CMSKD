﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.STSDetBCQ
{
  public class Create
  {
    public class Command : IRequest<STSDetBDTO>
    {
      public long IdSTS { get; set; }
      public long IdRek { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSTS).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, STSDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<STSDetB>(request);

        if (!await _context.STSDetB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.STSDetB
          .FindAllAsync<STS, DaftRekening>(
            x => x.IdSTSDetB == added.IdSTSDetB,
            x => x.STS, x => x.Rekening);

        return _mapper.Map<STSDetBDTO>(result.SingleOrDefault());
      }
    }
  }
}