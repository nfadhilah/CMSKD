using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.KodeBank
{
  public class Create
  {
    public class Command : IRequest<JBank>
    {
      // public long IdJBank { get; set; }
      public string KdBank { get; set; }
      public string NmBank { get; set; }
      public string Uraian { get; set; }
      public string Akronim { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdJBank).NotEmpty();
        RuleFor(d => d.KdBank).NotEmpty();
        RuleFor(d => d.NmBank).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
        RuleFor(d => d.Akronim).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JBank>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBank> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JBank>(request);

        if (!await _context.JBank.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}