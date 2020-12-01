using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TagihanCQ
{
  public class Create
  {
    public class Command : IRequest<TagihanDTO>
    {
      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public string NoTagihan { get; set; }
      public DateTime TglTagihan { get; set; }
      public long IdKontrak { get; set; }
      public string UraianTagihan { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.NoTagihan).NotEmpty();
        RuleFor(d => d.TglTagihan).NotEmpty();
        RuleFor(d => d.IdKontrak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, TagihanDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TagihanDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Tagihan>(request);

        if (!await _context.Tagihan.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.Tagihan
          .FindAllAsync<Kontrak>(
            x => x.IdTagihan == added.IdTagihan,
            x => x.Kontrak);

        return _mapper.Map<TagihanDTO>(result.SingleOrDefault());
      }
    }
  }
}