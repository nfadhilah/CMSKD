using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetRDana>
    {
      public long IdSPPDetR { get; set; }
      public string KdDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPPDetR).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetRDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetRDana>(request);

        if (!await _context.SPPDetRDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}