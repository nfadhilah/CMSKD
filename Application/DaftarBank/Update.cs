using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DaftarBank
{
  public class Update
  {
    public class Command : IRequest
    {
      public long IdBank { get; set; }
      public string KdBank { get; set; }
      public string AkBank { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string Cabang { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.KdBank).NotEmpty();
        RuleFor(d => d.AkBank).NotEmpty();
        RuleFor(d => d.Alamat).NotEmpty();
        RuleFor(d => d.Telepon).NotEmpty();
        RuleFor(d => d.Cabang).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
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
          await _context.DaftBank.FindAsync(x => x.IdBank == request.IdBank);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DaftBank.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}