﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRPCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetRP>
    {
      public long IdSPPDetR { get; set; }
      public long IdPajak { get; set; }
      public decimal? Nilai { get; set; }
      public string Keterangan { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglBilling { get; set; }
      public string NTPN { get; set; }
      public string NTB { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPPDetR).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetRP>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRP> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetRP>(request);

        if (!await _context.SPPDetRP.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}