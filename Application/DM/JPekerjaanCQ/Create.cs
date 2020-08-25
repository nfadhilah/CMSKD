using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JPekerjaanCQ
{
  public class Create
  {
    public class Command : IRequest<JPekerjaan>
    {
      public string Uraian { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.Uraian).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JPekerjaan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JPekerjaan> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JPekerjaan>(request);

        if (!await _context.JPekerjaan.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}