﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DaftRekeningCQ
{
  public class Create
  {
    public class Command : IRequest<DaftRekening>
    {
      public string KdPer { get; set; }
      public string NmPer { get; set; }
      public int MtgLevel { get; set; }
      public int KdKhusus { get; set; }
      public long IdJRek { get; set; }
      public long? IdJnsAkun { get; set; }
      public string Type { get; set; }
      public int? StAktif { get; set; }
    }

    public class Validator : AbstractValidator<DaftRekening>
    {
      public Validator()
      {
        RuleFor(d => d.KdPer).NotEmpty();
        RuleFor(d => d.NmPer).NotEmpty();
        RuleFor(d => d.MtgLevel).NotEmpty();
        RuleFor(d => d.KdKhusus).NotEmpty();
        RuleFor(d => d.JnsRek).NotEmpty();
        RuleFor(d => d.Type).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftRekening>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftRekening> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftRekening>(request);

        if (!await _context.DaftRekening.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}