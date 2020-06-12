using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Kegiatan
{
  public class Create
  {
    public class Command : IRequest<MKegiatan>
    {
      public long IdPgrm { get; set; }
      public string KdPerspektif { get; set; }
      public string NuKeg { get; set; }
      public string NmKegUnit { get; set; }
      public int LevelKeg { get; set; }
      public string Type { get; set; }
      public string KdKeg_Induk { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdPgrm).NotEmpty();
        RuleFor(d => d.KdPerspektif).NotEmpty();
        RuleFor(d => d.NuKeg).NotEmpty();
        RuleFor(d => d.NmKegUnit).NotEmpty();
        RuleFor(d => d.LevelKeg).NotEmpty();
        RuleFor(d => d.Type).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, MKegiatan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MKegiatan> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<MKegiatan>(request);

        if (!await _context.MKegiatan.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
