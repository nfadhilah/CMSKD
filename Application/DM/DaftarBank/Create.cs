using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DaftarBank
{
  public class Create
  {
    public class Command : IRequest<DaftBank>
    {
      // public long IdBank { get; set; }
      public string KdBank { get; set; }
      public string AkBank { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string Cabang { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.KdBank).NotEmpty();
        RuleFor(d => d.AkBank).NotEmpty();
        RuleFor(d => d.Alamat).NotEmpty();
        RuleFor(d => d.Telepon).NotEmpty();
        RuleFor(d => d.Cabang).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftBank>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftBank> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftBank>(request);

        if (!await _context.DaftBank.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}