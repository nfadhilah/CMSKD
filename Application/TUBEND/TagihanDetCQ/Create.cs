using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TagihanDetCQ
{
  public class Create
  {
    public class Command : IRequest<TagihanDetDTO>
    {
      public long IdTagihan { get; set; }
      public long IdRek { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdTagihan).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, TagihanDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TagihanDetDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<TagihanDet>(request);

        if (!await _context.TagihanDet.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.TagihanDet
          .FindAllAsync<DaftRekening>(
            x => x.IdTagihanDet == added.IdTagihanDet,
            x => x.Rekening);

        return _mapper.Map<TagihanDetDTO>(result.SingleOrDefault());
      }
    }
  }
}