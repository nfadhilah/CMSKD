using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.KegUnitCQ
{
  public class Tree
  {
    public class TreeDTO
    {
      public long? IdKeg { get; set; }
      public string Kode { get; set; }
      public string Label { get; set; }
      public int Lvl { get; set; }
      public string Type { get; set; }
    }

    public class Query : IRequest<IEnumerable<TreeDTO>>
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

    public class Handler : IRequestHandler<Query, IEnumerable<TreeDTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<TreeDTO>> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await
          _context.KegUnit.GetTreeKegUnit(request.IdUnit, request.KdTahap);

        var mapConfig = new MapperConfiguration(cfg => { });

        return _mapper.Map<IEnumerable<TreeDTO>>(result);
      }
    }
  }
}
