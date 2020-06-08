using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisDana
{
  public class Create
  {
    public class Command : IRequest<JDana>
    {
    // public long IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public string Ket { get; set; }
    public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdJDana).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
        RuleFor(d => d.NmDana).NotEmpty();
        RuleFor(d => d.Ket).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JDana>(request);

        if (!await _context.JDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}