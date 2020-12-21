using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TagihanDetCQ
{
  public class BulkInsert
  {
    public class Command : IRequest<IEnumerable<TagihanDetDTO>>
    {
      public IEnumerable<Create.Command> TagihanBulk { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.TagihanBulk).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, IEnumerable<TagihanDetDTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<TagihanDetDTO>> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<IEnumerable<TagihanDet>>(request.TagihanBulk).ToList();

        if (await _context.TagihanDet.BulkInsertAsync(added) == 0)
          throw new ApiException("Problem saving changes");

        var result = await _context.TagihanDet
          .FindAllAsync<DaftRekening>(
            x => added.Select(a => a.IdTagihanDet).Contains(x.IdTagihanDet),
            x => x.Rekening);

        return _mapper.Map<IEnumerable<TagihanDetDTO>>(result);
      }
    }
  }
}