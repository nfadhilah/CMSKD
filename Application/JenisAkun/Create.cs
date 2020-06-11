using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisAkun
{
  public class Create
  {
    public class Command : IRequest<JnsAkun>
    {
      // public long IdJnsAkun { get; set; }
      public string UraiAkun { get; set; }
      public string KdPers { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdJnsAkun).NotEmpty();
        RuleFor(d => d.UraiAkun).NotEmpty();
        RuleFor(d => d.KdPers).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JnsAkun>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JnsAkun> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JnsAkun>(request);

        if (!await _context.JnsAkun.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}