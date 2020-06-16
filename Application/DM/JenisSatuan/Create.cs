using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JenisSatuan
{
  public class Create
  {
    public class Command : IRequest<JSatuan>
    {
      // public long IdSatuan { get; set; }
      public string KdSatuan { get; set; }
      public string UraiSatuan { get; set; }
      public string Ket { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdSatuan).NotEmpty();
        RuleFor(d => d.KdSatuan).NotEmpty();
        RuleFor(d => d.UraiSatuan).NotEmpty();
        RuleFor(d => d.Ket).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JSatuan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JSatuan> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JSatuan>(request);

        if (!await _context.JSatuan.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}