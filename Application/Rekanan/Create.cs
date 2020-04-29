using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class Create
  {
    public class Command : IRequest
    {
      public string Kdp3 { get; set; }
      public string Nmp3 { get; set; }
      public string Nminst { get; set; }
      public string Norcp3 { get; set; }
      public string Nmbank { get; set; }
      public string Jnsusaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public string Unitkey { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.Kdp3).NotEmpty();
        RuleFor(d => d.Nmp3).NotEmpty();
        RuleFor(d => d.Nminst).NotEmpty();
        RuleFor(d => d.Norcp3).NotEmpty();
        RuleFor(d => d.Nmbank).NotEmpty();
        RuleFor(d => d.NPWP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftPhk3>(request);

        if (!await _context.DaftPhk3.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}