using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.BKUKCQ
{
  public class Create
  {
    public class Command : IRequest<BKUK>
    {
      public long? IdKas { get; set; }
      public long IdSP2D { get; set; }
      public long? IdBKas { get; set; }
      public long? IdUnit { get; set; }
      public DateTime? TglKas { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdSP2D).NotEmpty();
        RuleFor(d => d.IdKas).NotEmpty();
        RuleFor(d => d.IdBKas).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BKUK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUK> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BKUK>(request);

        if (!await _context.BKUK.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}