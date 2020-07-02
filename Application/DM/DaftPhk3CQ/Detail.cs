using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftPhk3CQ
{
  public class Detail
  {
    public class Query : IRequest<DaftPhk3DTO>
    {
      public int IdPhk3 { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftPhk3DTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3DTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.DaftPhk3
          .FindAllAsync<JBank, JUsaha>(x => x.IdPhk3 == request.IdPhk3,
            x => x.Bank, x => x.IdJUsaha)).SingleOrDefault();

        if (result == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DaftPhk3DTO>(result);
      }
    }
  }
}
