using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PPKSKPD
{
  public class Create
  {
    public class Command : IRequest<PPK>
    {
      // public long IdPPK { get; set; }
      public long IdUnit { get; set; }
      public long IdPeg { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdPPK).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
        RuleFor(d => d.DateUpdate).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, PPK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PPK> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PPK>(request);

        if (!await _context.PPK.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}