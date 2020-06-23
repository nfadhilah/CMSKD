using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SP2DDetRDana>
    {
      public long IdSP2DDetRDana { get; set; }
      public long IdSP2DDetR { get; set; }
      public long KdDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSP2DDetR).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2DDetRDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2DDetRDana>(request);

        if (!await _context.SP2DDetRDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}