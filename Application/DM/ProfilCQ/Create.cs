using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.ProfilCQ
{
  public class Create
  {
    public class Command : IRequest<Profil>
    {
      public string KdProfil { get; set; }
      public string NmProfil { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.KdProfil).NotEmpty();
        RuleFor(d => d.NmProfil).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Profil>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Profil> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Profil>(request);

        if (!await _context.Profil.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}