using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.UnitOrganisasi
{
  public class Create
  {
    public class Command : IRequest<DaftUnit>
    {
      public string KdUnit { get; set; }
      public string NmUnit { get; set; }
      public int KdLevel { get; set; }
      public string Type { get; set; }
      public string AkroUnit { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public int StAktif { get; set; }
      public DateTime DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(r => r.KdUnit).NotEmpty();
        RuleFor(r => r.NmUnit).NotEmpty();
        RuleFor(r => r.KdLevel).NotEmpty();
        RuleFor(r => r.Type).NotEmpty();
        RuleFor(r => r.StAktif).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftUnit> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftUnit>(request);

        if (!await _context.DaftUnit.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
