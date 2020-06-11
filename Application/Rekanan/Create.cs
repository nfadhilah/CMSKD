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
    public class Command : IRequest<DaftPhk3>
    {
      public string NmP3 { get; set; }
      public string NmInst { get; set; }
      public string NoRcP3 { get; set; }
      public string NmBank { get; set; }
      public string JnsUsaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public string IdUnit { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.NmP3).NotEmpty();
        RuleFor(d => d.NmInst).NotEmpty();
        RuleFor(d => d.NoRcP3).NotEmpty();
        RuleFor(d => d.NmBank).NotEmpty();
        RuleFor(d => d.NPWP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftPhk3>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftPhk3>(request);

        if (!await _context.DaftPhk3.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}