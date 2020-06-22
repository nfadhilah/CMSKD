using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BeritaCQ
{
  public class Detail
  {
    public class Query : IRequest<Berita>
    {
      public long IdBerita { get; set; }
    }

    public class Handler : IRequestHandler<Query, Berita>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Berita> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.Berita.FindByIdAsync(request.IdBerita);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
