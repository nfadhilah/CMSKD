using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.MetodePengadaanCQ
{
  public class Create
  {
    public class Command : IRequest<MetodePengadaan>
    {
      public string Kode { get; set; }
      public string Uraian { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.Kode).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, MetodePengadaan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MetodePengadaan> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<MetodePengadaan>(request);

        if (!await _context.MetodePengadaan.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}