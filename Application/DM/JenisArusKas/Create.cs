using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JenisArusKas
{
  public class Create
  {
    public class Command : IRequest<JAKas>
    {
      // public long IdKas { get; set; }
      public string KdAKas { get; set; }
      public string NmAKas { get; set; }
      public string LabelKas { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdKas).NotEmpty();
        RuleFor(d => d.KdAKas).NotEmpty();
        RuleFor(d => d.NmAKas).NotEmpty();
        RuleFor(d => d.LabelKas).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JAKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JAKas> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JAKas>(request);

        if (!await _context.JAKas.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}