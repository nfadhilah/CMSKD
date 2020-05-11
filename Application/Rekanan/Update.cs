using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class Update
  {
    public class Command : IRequest
    {
      public string KdP3 { get; set; }
      public string NmP3 { get; set; }
      public string NmInst { get; set; }
      public string NoRcP3 { get; set; }
      public string NmBank { get; set; }
      public string JnsUsaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public string UnitKey { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.KdP3).NotEmpty();
        RuleFor(d => d.NmP3).NotEmpty();
        RuleFor(d => d.NmInst).NotEmpty();
        RuleFor(d => d.NoRcP3).NotEmpty();
        RuleFor(d => d.NmBank).NotEmpty();
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

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DaftPhk3.FindByIdAsync(request.KdP3);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DaftPhk3.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}