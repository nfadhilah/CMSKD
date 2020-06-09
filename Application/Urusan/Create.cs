using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Urusan
{
  public class Create
  {

    public class Command : IRequest<UrusanUnit>
    {
      public long IdUnit { get; set; }
      public long UrusKey { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(x => x.IdUnit).NotEmpty();
        RuleFor(x => x.UrusKey).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, UrusanUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<UrusanUnit> Handle(Command request, CancellationToken cancellationToken)
      {
        if (await _context.DaftUnit.FindByIdAsync(request.IdUnit) == null)
          throw new ApiException("Unit tidak ditemukan", (int)HttpStatusCode.NotFound);

        if (await _context.DaftUnit.FindByIdAsync(request.UrusKey) == null)
          throw new ApiException("Urusan tidak ditemukan", (int)HttpStatusCode.NotFound);

        var added = _mapper.Map<UrusanUnit>(request);

        if (!await _context.UrusanUnit.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
