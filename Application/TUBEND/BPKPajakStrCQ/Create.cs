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

namespace Application.TUBEND.BPKPajakStrCQ
{
  public class Create
  {
    public class Command : IRequest<BPKPajakStrDTO>
    {
      public long? IdBPKDetRP { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBPKDetRP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BPKPajakStrDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKPajakStrDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BPKPajakStr>(request);

        if (!await _context.BPKPajakStr.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BPKPajakStr
          .FindAllAsync<BPKDetRP>(
            x => x.IdBkPajakStr == added.IdBkPajakStr,
            x => x.BPKDetRp);

        return _mapper.Map<BPKPajakStrDTO>(result.SingleOrDefault());
      }
    }
  }
}