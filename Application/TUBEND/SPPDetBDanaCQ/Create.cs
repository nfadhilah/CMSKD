using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetBDana>
    {
      public long IdSPPDetB { get; set; }
      public string KdDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPPDetB).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetBDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetBDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetBDana>(request);

        if (!await _context.SPPDetBDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}