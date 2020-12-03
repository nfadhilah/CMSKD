using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DocMetaCQ
{
  public class Create
  {
    public class Command : IRequest<DocMeta>
    {
      public string KdDok { get; set; }
      public string NoDok { get; set; }
      public string FilePath { get; set; }
      public int? Status { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.KdDok).NotEmpty();
        RuleFor(x => x.NoDok).NotEmpty();
        RuleFor(x => x.FilePath).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DocMeta>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DocMeta> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DocMeta>(request);

        if (!await _context.DocMeta.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.DocMeta
          .FindAllAsync(
            x => x.Id == added.Id);

        return result.SingleOrDefault();
      }
    }
  }
}
