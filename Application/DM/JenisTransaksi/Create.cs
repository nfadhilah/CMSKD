using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JenisTransaksi
{
  public class Create
  {
    public class Command : IRequest<JTrans>
    {
      // public long IdJTrans { get; set; }
      public string IdTrans { get; set; }
      public string NmTrans { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdJTrans).NotEmpty();
        RuleFor(d => d.IdTrans).NotEmpty();
        RuleFor(d => d.NmTrans).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JTrans>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JTrans> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JTrans>(request);

        if (!await _context.JTrans.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}