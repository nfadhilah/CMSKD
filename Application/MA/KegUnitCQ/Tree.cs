using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.KegUnitCQ
{
  public class Tree
  {

    public class Query : IRequest<object>
    {
      public long IdUnit { get; set; }
      public string KdTahap { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.IdUnit).NotEmpty();
        RuleFor(x => x.KdTahap).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, object>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<object> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await
          _context.KegUnit.GetTreeKegUnit(request.IdUnit, request.KdTahap);

        return result;
      }
    }
  }
}
