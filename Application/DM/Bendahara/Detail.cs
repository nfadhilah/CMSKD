using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.Bendahara
{
  public class Detail
  {

    public class Query : IRequest<Bend>
    {
      public string IdBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, Bend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Bend> Handle(
      Query request, CancellationToken cancellationToken)
      {
        // var result =
        //   await _context.Bend.FindByIdAsync(request.IdBend);
            var result=
          (await _context.Bend.FindAllAsync<Pegawai>(
            x => x.IdBend == request.IdBend, c => c.Pegawai)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
